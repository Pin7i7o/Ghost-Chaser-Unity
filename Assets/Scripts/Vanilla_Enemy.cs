using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vanilla_Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player, flashLight;

    public LayerMask groundMask, playerMask, enemyMask;

    //Stats
    public int hp, damage;

    //Kills UI
    KillCounter killCounter;

    //Weapon
    public GameObject plasma;
    public Transform shootPoint;
    public float shootSpeed;
    public float attackCooldown;
    float cooldown;

    //Patrolling State
    public Vector3 walkPoint;
    bool isWalkPointSet;
    public float walkPointRange;

    //State Ranges
    public float sightRange, attackRange;
    bool isPlayerInSightRange, isPlayerInAttackRange;

    //EnemyFrozen
    bool isFrozen;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        flashLight = GameObject.Find("FlashLight").transform;
        killCounter = GameObject.Find("KCO").GetComponent<KillCounter>();
        cooldown = attackCooldown;
    }

    private void Update()
    {
        //Is player in attack or sight range?
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
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

        if (!isPlayerInSightRange && !isPlayerInAttackRange && isFrozen == false) PatrollingPhase();
        if (isPlayerInSightRange && !isPlayerInAttackRange && isFrozen == false) ChassingPhase();
        if (isPlayerInSightRange && isPlayerInAttackRange && isFrozen == false) AttackingPhase();

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

        if(distanceToWalkPoint.magnitude < 1f)
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

    private void ChassingPhase()
    {
        agent.SetDestination(player.position);
    }

    private void AttackingPhase()
    {
        //Enemy doesn't move when attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        attackCooldown -= Time.deltaTime;

        if(attackCooldown < 0)
        {
            ShootPlasma();
            attackCooldown = cooldown;
        }
    }

    private void ShootPlasma()
    {
        GameObject currentPlasma = Instantiate(plasma, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = currentPlasma.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * shootSpeed, ForceMode.Impulse);
        rb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
