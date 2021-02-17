using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject currentPlatform;
    [SerializeField] private int startingPlatformCount;
    [SerializeField] private GameObject waypointPrefab;

    int nextPlatformDirection;

    public static PathGenerator instance;
    public List<Transform> waypoints; // Actually dont need them, remmove it.
    public Bounds planeBounds;
    

    private void OnEnable()
    {
        instance = this;
        Plane();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPlatform.transform.position = new Vector3(Random.Range(0, 5), 0, 0);
        GeneratePath();

    }

    private void GeneratePath()
    {
        
        for (int i = 0; i < startingPlatformCount; i++)
        {
           if(planeBounds.Contains(currentPlatform.transform.position))
           {
                nextPlatformDirection = Random.Range(0, 2);

                if(nextPlatformDirection == 0)
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + Vector3.right, Quaternion.identity);
                }
                else
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + Vector3.forward, Quaternion.identity);
                }
                waypoints.Add(currentPlatform.transform);
                ShowWaypoints(); // dont need this 
           }
        }
    }

    private void ShowWaypoints() //  and this too
    {
        foreach (var waypointTransform in waypoints)
        {
            Instantiate(waypointPrefab, waypointTransform.position, Quaternion.identity);
        }
    }


    private void Plane()
    {
        planeBounds = new Bounds();

        planeBounds.min = new Vector3(0, 0, 0);
        planeBounds.max = new Vector3(200, 0, 200);

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.localScale = planeBounds.size;
        plane.transform.localPosition = planeBounds.center;
    }
    
}
