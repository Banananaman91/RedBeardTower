using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] public GameObject currentPlatform;
    [SerializeField] private int startingPlatformCount;
    [SerializeField] private GameObject waypointPrefab;
    

    int nextPlatformDirection;

    public static PathGenerator instance;
    public Bounds planeBounds;
    Vector3 midMin;
    Vector3 midMax;
    int wholeN;
    int wholeNMax;
    public NavMeshSurface navMeshSurface;
    EnemyManager enemyManager;
    public GameObject basicEnemyPrefab;
    public static List<Vector3> PathTransforms;




    private void OnEnable()
    {
        Init();//Initialisers
        GeneratePlane();
    }


    // Start is called before the first frame update
    void Start()
    {
        GeneratePath();
        ShowNavMesh();
    }

    private void Init()
    {
        instance = this;
        planeBounds = new Bounds();
        enemyManager = FindObjectOfType<EnemyManager>();
        PathTransforms = new List<Vector3>();
    } 

    private void GeneratePlane()
    {
        // Set the bounds of the plane
        planeBounds.min = new Vector3(0, 0, 0);
        planeBounds.max = new Vector3(200, 0, 200);
        
        //instatnite the plane and configure it to resaemble the bounds plane.
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.localScale = planeBounds.size;
        plane.transform.localPosition = planeBounds.center;
       
        //Add NavmeshObsitcle.
        plane.AddComponent<NavMeshObstacle>();

        // Calculate the center third of the plane to instantiate the starting pathPlatform.
        midMin = planeBounds.max / 3;
        midMax = planeBounds.max - midMin;
        wholeN = (int)midMin.x;
        wholeNMax = (int)midMax.x;
    }

    private void GeneratePath()
    {
        currentPlatform.transform.position = new Vector3(Random.Range(wholeN, wholeNMax), 0, 0);
        if (currentPlatform.transform.position == null) { return; }

        for (int i = 0; i < startingPlatformCount; i++)
        {
            if (planeBounds.Contains(currentPlatform.transform.position))
            {
                nextPlatformDirection = Random.Range(0, 2);   //------ Could add more variations to the path, but for now am leaving it to 3 dir.

                if (nextPlatformDirection == 0)
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + Vector3.right, Quaternion.identity);
                    PathTransforms.Add(currentPlatform.transform.position);

                }
                if (nextPlatformDirection == 1)
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + -Vector3.right, Quaternion.identity);
                    PathTransforms.Add(currentPlatform.transform.position);

                }
                else
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + Vector3.forward, Quaternion.identity);
                    PathTransforms.Add(currentPlatform.transform.position);

                }
            }
        }   
    }


    private void ShowNavMesh()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>(); //------ seems to be working just fine.
        navMeshSurface.BuildNavMesh();

        Vector3 firstPosToSpawn = PathTransforms[0];

        enemyManager.StartEnemySpawn(basicEnemyPrefab, firstPosToSpawn);
       
    }
    
}
