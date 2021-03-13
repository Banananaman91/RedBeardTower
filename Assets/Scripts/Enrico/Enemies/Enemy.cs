using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public HealthBar HealthBar;
    public int maxHealth = 100;
    public int coinValue;

    private GameObject goal;
    private GameManager GM;
    private int currentHealth;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    private void Start()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        goal = GameObject.FindObjectOfType<GoalHolder>().gameObject;

        agent.destination = goal.transform.position;
    }

    public void TakeDamageAndDestroy(int damage)
    {
        currentHealth -= damage;
        HealthBar.SetHealth(currentHealth);
       
        if(currentHealth == 0)
        {
            GM.Coins += coinValue;
            Destroy(this.gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {

            TakeDamageAndDestroy(10);  // Add damage according to tower type??
        }
    }
}
