using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isEnemyHit = false;

    private void Update()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit the enemy");
            Destroy(other.gameObject);
        }
    }
}
