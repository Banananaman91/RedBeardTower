using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    
    private Transform camPos;
   
    void LateUpdate()
    {
        camPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.LookAt(transform.position + camPos.forward);
    }
}
