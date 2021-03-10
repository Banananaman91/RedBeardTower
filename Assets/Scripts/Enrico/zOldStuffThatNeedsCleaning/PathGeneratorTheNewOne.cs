using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class PathGeneratorTheNewOne : MonoBehaviour
{
    ObjectPlacementController OP => FindObjectOfType<ObjectPlacementController>();

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] public GameObject currentPlatform;
    [SerializeField] private int startingPlatformCount;
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] public GameObject NewPlane;
    [SerializeField] public GameObject startPoint;

    public Text debugText;
    int nextPlatformDirection;

   
    public static PathGeneratorTheNewOne instance;

    public Bounds planeBounds;
    private Vector3 midMin;
    private Vector3 midMax;
    private int wholeN;
    private int wholeNMax;

    public NavMeshSurface navMeshSurface;
    private EnemyManager enemyManager => FindObjectOfType<EnemyManager>();
    public GameObject basicEnemyPrefab;
    public static List<GameObject> PathTransforms;

  

    private void OnEnable()
    {
        Init();//Initialisers

    }


    // Start is called before the first frame update
    void Start()
    {
        GeneratePath();
        ShowNavMesh();
    }

    private void Update()
    {
       
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
        planeBounds.min = new Vector3(0, 0, 0);
        planeBounds.max = new Vector3(1f, 0, 1f);

       // if (PB.arPlaneSize == null) { return; }

        //instatnite the plane and configure it to resaemble the bounds plane.

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.localScale = planeBounds.size;
        plane.transform.localPosition = planeBounds.center;


        //planeBounds.size = new Vector3(PB.arPlaneSize.x, 0, PB.arPlaneSize.y);
        //planeBounds.center = PB.arPlaneCenter;
        ////planeBounds.extents = PB.arPlaneExt;
        //plane = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
        //plane.transform.localScale = planeBounds.size;
        //// plane.transform.position = PB.arPlaneTransform.position;
        //plane.transform.localPosition = PB.arPlaneTransform.localPosition;
        //plane.transform.localRotation = PB.arPlaneTransform.localRotation;


        //Add NavmeshObsitcle.
        plane.AddComponent<NavMeshObstacle>();

        // Calculate the center third of the plane to instantiate the starting pathPlatform.
        midMin = planeBounds.max / 3;
        midMax = planeBounds.max - midMin;
        wholeN = (int)midMin.x;
        wholeNMax = (int)midMax.x;

        //GeneratePath();
        //ShowNavMesh();
    }

    public void GeneratePath()
    {
        //currentPlatform.transform.position = new Vector3(planeBounds.extents.x*2,planeBounds.extents.y*2,planeBounds.extents.z*2);
        //currentPlatform.transform.position = plane.transform.localPosition;
        //currentPlatform.transform.position = new Vector3(Random.Range(planeBounds.min.x, planeBounds.max.x), plane.transform.position.y, plane.transform.position.z);
        //currentPlatform.transform.position = new Vector3(OP.hitObject.transform.position.x, OP.hitObject.transform.position.y , OP.hitObject.transform.position.z);
        //currentPlatform.transform.position = new Vector3(planeBounds.extents.x, 0, planeBounds.extents.z);
        // currentPlatform.transform.position = new Vector3(planeBounds.extents.x, 0, planeBounds.extents.z);
        //currentPlatform.transform.position = new Vector3(plane.transform.localPosition.x, 0, plane.transform.localPosition.y);

        currentPlatform.transform.position = new Vector3(Random.Range(wholeN, wholeNMax), 0, 0);
        if (currentPlatform.transform.position == null) { return; }

        for (int i = 0; i < startingPlatformCount; i++)
        {
            debugText.text = "cur " + currentPlatform.transform.position + " plane " + planeBounds.size.ToString(); ;
            
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

       // enemyManager.StartEnemySpawn(basicEnemyPrefab, firstPosToSpawn);

       // RemovePathPlatforms(); // remove all the unnescessary tiles.. navmesh needs only the last tile to store the whole path
    }

    public void RemovePathPlatforms()
    {
        for (int i = 0; i < PathTransforms.Count -1; i++)
        {
            Destroy(PathTransforms[i]);
        }
    }

}
