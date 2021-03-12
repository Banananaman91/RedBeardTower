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
}
