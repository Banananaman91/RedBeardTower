using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{

    public void StartEnemySpawn(GameObject ObjectToSpawn, Vector3 position)
    {
        GameObject objSpwnd = Instantiate(ObjectToSpawn, position, Quaternion.identity);
        objSpwnd.transform.position = new Vector3(objSpwnd.transform.position.x, 0, objSpwnd.transform.position.y);
    }

}
