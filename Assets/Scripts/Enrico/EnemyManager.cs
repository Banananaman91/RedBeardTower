using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{
    public GameObject startPos;
    public GameObject basicEnemy;

    private void Update()
    {
        startPos = GameObject.FindObjectOfType<StartHolder>().gameObject ;
        Debug.Log(startPos.transform.localPosition);
        if( startPos!=null)
        StartEnemySpawn(basicEnemy, startPos.transform.position);
    }
    private void Start()
    {

    }

   
    public void StartEnemySpawn(GameObject ObjectToSpawn, Vector3 position)
    {
        Instantiate(ObjectToSpawn, position, Quaternion.identity);
    }

}
