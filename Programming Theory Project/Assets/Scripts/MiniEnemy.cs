using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class MiniEnemy : Enemy
{
    //POLYMORPHISM
    public override void FollowPlayer(GameObject player, Rigidbody enemyR)
    {
        Vector3 lookDirection = (player.transform.position - gameObject.transform.position).normalized;
        enemyR.AddForce(lookDirection * speed * Time.deltaTime * 100);
        enemyR.drag = 0;
        StartCoroutine("DragCountdown");
    }
}
