using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject goal;

    public HealthBar HealthBar;
    public int maxHealth = 100;
    public int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        SetDestination(); // to put on Awake???
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
            Destroy(this.gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            TakeDamageAndDestroy(10);  // Add damage according to tower type??
        }
    }
}
