using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemyTo : MonoBehaviour
{
    Vector3 goal;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        var lastPlatformTrans = PathGenerator.PathTransforms.Count - 1;
        goal = PathGenerator.PathTransforms[lastPlatformTrans];

        agent.destination = goal;
    }
}
