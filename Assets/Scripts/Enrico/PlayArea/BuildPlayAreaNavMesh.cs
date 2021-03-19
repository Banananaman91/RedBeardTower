using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildPlayAreaNavMesh : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    
    private void Update()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>(); //------ seems to be working just fine.
        navMeshSurface.BuildNavMesh();
    }
  
}
