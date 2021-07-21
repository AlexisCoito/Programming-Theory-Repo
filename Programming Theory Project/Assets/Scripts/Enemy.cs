using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody enemyRb;
    public float speed = 10;
    private GameObject player;
    protected bool hasToFollow = true;
    public GameObject remains;
    protected Rigidbody remainRb;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        remainRb = remains.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasToFollow)
        {
            // ABSTRACTION
            FollowPlayer(player, enemyRb);   
        }
    }

    public virtual void FollowPlayer(GameObject player, Rigidbody enemyR)
    {
        Vector3 lookDirection =(player.transform.position - gameObject.transform.position).normalized;
        enemyR.AddForce(lookDirection * speed * Time.deltaTime * 100);
        enemyR.drag = 0;
        hasToFollow = false;
        StartCoroutine("FollowCountdown");
        StartCoroutine("DragCountdown");
    }

    public IEnumerator FollowCountdown()
    {
        yield return new WaitForSeconds(3);

        hasToFollow = true;
    }

    public IEnumerator DragCountdown()
    {
        yield return new WaitForSeconds(2);
        enemyRb.drag = 1;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            for (int i = 0; i < (Random.Range(0, 3)); i++)
            {
                Instantiate(remains, transform.position, transform.rotation);
                remainRb.velocity = enemyRb.velocity; 
            }
            
            Destroy(gameObject);
        }

        
    }
}
