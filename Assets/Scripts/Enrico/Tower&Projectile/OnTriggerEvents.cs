﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEvents : MonoBehaviour
{
    GameManager GM;

    private void Awake()
    {
        if(GM != null) { return; }
        GM = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SimpleEnemy" ||
            other.gameObject.tag == "HardEnemy" ||
            other.gameObject.tag == "FastEnemy")
        {
           
            Destroy(other.gameObject);
            EnemyManager.enemiesAlive--;
           // GM.Lives -= EnemyManager.basicEnemyDamage;
            GM.PlayerCurrentLives -= EnemyManager.basicEnemyDamage; ///Change to enemy script to hold the damagetaken value?? so each enemy has its own?
            GM.playerHealtBar.SetHealth(GM.PlayerCurrentLives);
        }
        // Add  More for other enemies and damamge.
    }
}
