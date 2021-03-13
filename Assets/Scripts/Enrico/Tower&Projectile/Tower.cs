using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject[] shootPoint;
    public GameObject projectile;
    public int projectileForce = 10;
    public float rateOfFire = 2;
    public GameObject targetToShoot;

    private void Update()
    {
        if (targetToShoot)
        {
            Vector3 direction = targetToShoot.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), 5* Time.smoothDeltaTime);
        }
        else
        {
            //transform.rotation =  Quaternion.Euler(0, 0, 0);
           // Scan();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targetToShoot = other.gameObject;
            InvokeRepeating("Shoot", 0, rateOfFire);
        }
        else if(other == null)
        {
            CancelInvoke("Shoot");
        }
    }


    private void OnTriggerExit(Collider obj)
    {
        if(obj == targetToShoot)
        {
            targetToShoot = null;
            CancelInvoke("Shoot");
            //Destroy(projectile);
        }
    }

    public void Shoot()
    {   
        foreach (var point in shootPoint)
        {
            if (targetToShoot)
            {
                Vector3 aim = (targetToShoot.transform.position - transform.position).normalized;
                var projectileInstance = Instantiate(projectile, point.transform.position, point.transform.rotation);
            
                var projectileRb = projectileInstance.GetComponent<Rigidbody>();
                projectileRb.AddForce(aim * projectileForce);
                //projectileRb.AddForce(shootPoint.transform.forward * projectileForce);
            }
        }
    }

    private void Scan()
    {
       //MAKE A SCAN FUNCTION
    }
}
