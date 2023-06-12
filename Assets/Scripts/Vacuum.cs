using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    //Gun Stats
    public int damage;
    
    [SerializeField]
    private GameObject particles;

    private void Start()
    {
        particles.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            particles.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            particles.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Input.GetMouseButton(0))
            {
                other.GetComponent<Vanilla_Enemy>().TakeDamage(damage);
            }
            
        }

        if (other.gameObject.CompareTag("ScaredEnemy"))
        {
            if (Input.GetMouseButton(0))
            {
                other.GetComponent<Retreat_Enemy>().TakeDamage(damage);
            }

        }
    }
}
