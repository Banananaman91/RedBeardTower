using System.Collections;
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
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
           // GM.Lives -= EnemyManager.basicEnemyDamage;
            GM.PlayerCurrentLives -= EnemyManager.basicEnemyDamage;
            GM.playerHealtBar.SetHealth(GM.PlayerCurrentLives);
        }
        // Add  More for other enemies and damamge.
    }
}
