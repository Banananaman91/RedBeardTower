using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemyTo : MonoBehaviour
{
    GameObject goal;

    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        goal = GameObject.FindObjectOfType<GoalHolder>().gameObject;

        agent.destination = goal.transform.position;
    }
}
