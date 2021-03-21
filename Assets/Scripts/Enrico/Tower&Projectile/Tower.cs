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

    public GameObject radiusSphere;
    private float radius;
    AudioSource audio;

    private void Awake()
    {
        //PUT IN UPDATE FOR DYNAMIC SCALING
        radius = GetComponent<SphereCollider>().radius;
        radiusSphere.transform.localScale = new Vector3(radius, radius, radius)*2;
        audio = GetComponent<AudioSource>();

    }

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
        if (other.gameObject.tag == "SimpleEnemy" ||
            other.gameObject.tag == "HardEnemy" ||
            other.gameObject.tag == "FastEnemy")
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
                audio.PlayOneShot(audio.clip);
                var projectileRb = projectileInstance.GetComponent<Rigidbody>();
                projectileRb.AddForce(aim * projectileForce);
            }
        }
    }

    private void Scan()
    {
       //MAKE A SCAN FUNCTION
    }
}
