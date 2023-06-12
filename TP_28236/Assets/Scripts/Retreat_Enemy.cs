using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Retreat_Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player, flashLight;

    public LayerMask groundMask, playerMask, enemyMask;

    //Patrolling State
    public Vector3 walkPoint;
    bool isWalkPointSet;
    public float walkPointRange;

    //EnemyKilled
    KillCounter killCounter;

    //EnemyFrozen
    bool isFrozen;

    //State Ranges
    public float sightRange;
    bool isPlayerInSightRange;

    //Stats
    public int hp;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        flashLight = GameObject.Find("FlashLight").transform;
        killCounter = GameObject.Find("KCO").GetComponent<KillCounter>();
    }

    private void Update()
    {
        //Is player in attack or sight range?
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);

        //Enemy Fozen if gets it by light
        if (Physics.Raycast(flashLight.position, flashLight.TransformDirection(Vector3.forward), 5f, enemyMask))
        {
            isFrozen = true;
            agent.updatePosition = false;
            agent.updateRotation = false;
        }
        else
        {
            agent.updatePosition = true;
            agent.updateRotation = true;
            isFrozen = false;
        }

        if (!isPlayerInSightRange && isFrozen == false) PatrollingPhase();
        if (isPlayerInSightRange && isFrozen == false) RetreatPhase();
    }

    private void PatrollingPhase()
    {
        if (!isWalkPointSet) SetWalkpoint();

        if (isWalkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached

        if (distanceToWalkPoint.magnitude < 1f)
        {
            isWalkPointSet = false;
        }
    }

    private void SetWalkpoint()
    {
        //New walkPoint
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            isWalkPointSet = true;
        }
    }


    private void RetreatPhase()
    {
        Vector3 playerDirection = transform.position - player.position;

        Vector3 newPosition = transform.position + playerDirection;

        agent.SetDestination(newPosition);
    }

    public void TakeDamage(int damage)
    {
        player.GetComponent<PlayerHP>().RegenHealth(1);
        hp -= damage;

        if (hp <= 0)
        {
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }

    private void DestroyEnemy()
    {
        killCounter.AddKill();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

