using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    public int damage;
    private PlayerHP dmgDealt;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            dmgDealt = collision.collider.GetComponent<PlayerHP>();

            if(dmgDealt != null)
            {
                dmgDealt.TakeDamage(damage);
            }
        }

        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }
}
