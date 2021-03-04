using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] public GameObject currentPlatform;
    [SerializeField] private int startingPlatformCount;
    [SerializeField] private GameObject waypointPrefab;

    public Text debugText;
    int nextPlatformDirection;


    public static PathGenerator instance;
    public Bounds planeBounds;
    private Vector3 midMin;
    private Vector3 midMax;
    private int wholeN;
    private int wholeNMax;
    public NavMeshSurface navMeshSurface;
    private EnemyManager enemyManager => FindObjectOfType<EnemyManager>();
    public GameObject basicEnemyPrefab;
    public static List<GameObject> PathTransforms;
    GameObject plane;

    ARPlaneManager PM;
    
    PlaneAreaBehaviour PB;
    bool planeFound = false;

    private void OnEnable()
    {
        Init();//Initialisers

    }


    // Start is called before the first frame update
    void Start()
    {
        //GeneratePath();
       // ShowNavMesh();
    }

    private void Update()
    {
        if (PB != null)
        {
            GeneratePlane();
        }
        else
        {
            PB = FindObjectOfType<PlaneAreaBehaviour>();
        }
        debugText.text = "ARROT " + PB.arPlaneTransform.rotation + " ROT " + plane.transform.rotation +" LOAR " + PB.arPlaneTransform.localRotation+ " LO " + plane.transform.localRotation;
    }
    private void Init()
    {
        instance = this;
        planeBounds = new Bounds();
        PathTransforms = new List<GameObject>();
    } 

    public void GeneratePlane()
    {

        // Set the bounds of the plane

        //planeBounds.min = new Vector3(0, 0, 0);
        //planeBounds.max = new Vector3(100, 0, 100);
        
        if (PB.arPlaneSize == null) { return; }
        planeBounds.size = new Vector3(PB.arPlaneSize.x, 0, PB.arPlaneSize.y);
        
        //instatnite the plane and configure it to resaemble the bounds plane.

        //plane.transform.localScale = planeBounds.size;
        //plane.transform.localPosition = planeBounds.center;

        //plane.transform.localScale = PB.arPlaneSize;              WORKED, POS TO CENTER AND SCALING.
        //plane.transform.localPosition = PB.arPlaneCenter;

        //plane.transform.localScale = PB.arPlaneSize;
        //plane.transform.position = PB.arPlaneTransform.position;
        //plane.transform.localRotation = PB.arPlaneTransform.localRotation;

        plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.AddComponent<Renderer>();
        plane.GetComponent<Renderer>().material.color = Color.green;
        plane.transform.localScale = planeBounds.size;
        plane.transform.position = PB.arPlaneTransform.position;
        plane.transform.localRotation = PB.arPlaneTransform.localRotation;
        
        //Add NavmeshObsitcle.
        plane.AddComponent<NavMeshObstacle>();

        // Calculate the center third of the plane to instantiate the starting pathPlatform.
        midMin = planeBounds.max / 3;
        midMax = planeBounds.max - midMin;
        wholeN = (int)midMin.x;
        wholeNMax = (int)midMax.x;

        GeneratePath();
        ShowNavMesh();
    }

    public void GeneratePath()
    {
        currentPlatform.transform.position = new Vector3(Random.Range(wholeN, wholeNMax), 0, 0);

        if (currentPlatform.transform.position == null) { return; }

        for (int i = 0; i < startingPlatformCount; i++)
        {
            if (planeBounds.Contains(currentPlatform.transform.position))
            {
                nextPlatformDirection = Random.Range(0, 2);   //0,1,2------ Could add more variations to the path, but for now am leaving it to 3 dir.

                if (nextPlatformDirection == 0)
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + Vector3.right, Quaternion.identity);
                    //PathTransforms.Add(currentPlatform.transform.position);
                    PathTransforms.Add(currentPlatform);

                }
                if (nextPlatformDirection == 1)
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + -Vector3.right, Quaternion.identity);
                    //PathTransforms.Add(currentPlatform.transform.position);
                    PathTransforms.Add(currentPlatform);

                }
                else
                {
                    currentPlatform = Instantiate(platformPrefab, currentPlatform.transform.position + Vector3.forward, Quaternion.identity);
                    // PathTransforms.Add(currentPlatform.transform.position);
                    PathTransforms.Add(currentPlatform);
                }
            }
        }
    }

    public void ShowNavMesh()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>(); //------ seems to be working just fine.
        navMeshSurface.BuildNavMesh();

        Vector3 firstPosToSpawn = PathTransforms[0].transform.position;

        enemyManager.StartEnemySpawn(basicEnemyPrefab, firstPosToSpawn);

        RemovePathPlatforms(); // remove all the unnescessary tiles.. navmesh needs only the last tile to store the whole path
    }

    public void RemovePathPlatforms()
    {
        for (int i = 0; i < PathTransforms.Count -1; i++)
        {
            Destroy(PathTransforms[i]);
        }
    }

}
