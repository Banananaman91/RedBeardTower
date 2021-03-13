using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{
    public GameObject startPos;
    public GameObject basicEnemy;

    public static int basicEnemyDamage = 1;

    private void Start()
    {
        InvokeRepeating("spawn", 5, 3); // FKING MAKE WAVES!!!
    }

    private void Update()
    {
        if(startPos != null) { return; }
        startPos = GameObject.FindObjectOfType<StartHolder>().gameObject;
    }
    

    public void spawn()
    {
        Instantiate(basicEnemy, startPos.transform.position, startPos.transform.rotation);
    }

    //public void StartEnemySpawn(GameObject ObjectToSpawn, Vector3 position)
    //{
    //    Instantiate(ObjectToSpawn, position, Quaternion.identity);
    //}
    
}

