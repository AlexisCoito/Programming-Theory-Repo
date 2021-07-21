using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// INHERITANCE
public class Boss : Enemy
{
    // POLYMORPHISM
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            for (int i = 0; i < (Random.Range(0, 3)); i++)
            {
                Instantiate(remains, new Vector3(transform.position.x + (i*2), transform.position.y + (i*2), transform.position.z + (i*2)), transform.rotation);
                remainRb.velocity = enemyRb.velocity;
            }

            Destroy(gameObject);
        }
    }
}
