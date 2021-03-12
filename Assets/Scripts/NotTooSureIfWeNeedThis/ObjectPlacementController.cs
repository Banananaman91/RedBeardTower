using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ObjectPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    

    [SerializeField]
    private Camera arCamera;
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private PlacementObject lastSelectedObject;
    private GameObject PlacedPrefab{ get; set; }

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        
    }

    void Update()
    {
        GetObjPosFromScreen();
        PlaceObjectsinScene();
    }


    private void GetObjPosFromScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();

                    if (lastSelectedObject != null)
                    {
                        lastSelectedObject.GetComponent<Renderer>().material.color = Color.red;
                        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        foreach (PlacementObject placementObject in allOtherObjects)
                        {
                            placementObject.IsSelected = placementObject == lastSelectedObject;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lastSelectedObject.GetComponent<Renderer>().material.color = Color.green;
                lastSelectedObject.IsSelected = false;
            }
        }
    }
   

    private void PlaceObjectsinScene()
    {
        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (lastSelectedObject == null)
            {
                lastSelectedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
            }
            else
            {
                if (lastSelectedObject.IsSelected)
                {
                    lastSelectedObject.transform.position = hitPose.position;
                    lastSelectedObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }

  
}
