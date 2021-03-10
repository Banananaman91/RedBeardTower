using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class NewObjPlacement : MonoBehaviour
{

    public GameObject ObjtoSpawn;

    private GameObject spwanedObjs;
    private ARRaycastManager raycastManager;

    Vector2 touchPos;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARPlaneManager aRPlaneManager;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    bool TryGetTouchPos( out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPos(out Vector2 touchPosition)) { return; }

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if( spwanedObjs == null)
            {
                spwanedObjs = Instantiate(ObjtoSpawn, hitPose.position, hitPose.rotation);

                foreach(var plane in aRPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                    aRPlaneManager.enabled = !aRPlaneManager.enabled;
                }
            }
        }
    }
}
