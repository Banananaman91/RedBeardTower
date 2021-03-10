using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    GameObject goal;

    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        goal = GameObject.FindObjectOfType<GoalHolder>().gameObject;

        agent.destination = goal.transform.position;
        checkPosition();
    }

     void checkPosition()
     {
        if (gameObject.transform.position == goal.transform.position)
        {
            Debug.Log("SamePOSSSSSSSSSSSs");
        }

     }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            HitByBullet();// TO DOD
            
        }
    }

    private void HitByBullet()
    {
        Destroy(this.gameObject, 0.1f);
    }
}
