using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
   
    public NavMeshSurface surface;

    private void Awake()
    {
        surface.BuildNavMesh();   //needs to be invoked to solve the error
    }
}

