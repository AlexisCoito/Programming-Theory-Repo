using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    // ENCAPSULATION
    private int cantEnemies { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        cantEnemies = 4;
        // ABSTRACTION
        InvokeRepeating("SpawnEnemies", 2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        int index = Random.Range(0, enemies.Length);
        for (int i = 0; i < Random.Range(0, cantEnemies); i++)
        {
            Instantiate(enemies[index], new Vector3(player.transform.position.x + Random.Range(10, 25), player.transform.position.y + 10, player.transform.position.z + Random.Range(10, 25)), enemies[index].transform.rotation);
        }
        cantEnemies++;
    }
}
