using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{
    public GameObject startPos;
    public GameObject basicEnemy;

    private void Start()
    {
        InvokeRepeating("spawn", 5, 3);
    }

    private void Update()
    {
        startPos = GameObject.FindObjectOfType<StartHolder>().gameObject;
      
    }
    
    //public void StartEnemySpawn(GameObject ObjectToSpawn, Vector3 position)
    //{
    //    Instantiate(ObjectToSpawn, position, Quaternion.identity);
    //}

    public void spawn()
    {
        Instantiate(basicEnemy, startPos.transform.position, startPos.transform.rotation);
    }

    
}

