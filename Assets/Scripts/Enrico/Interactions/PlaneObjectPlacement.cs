using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using VoxelTerrain.Voxel;
using UnityEngine.UI;
[RequireComponent(typeof(ARRaycastManager))]
public class PlaneObjectPlacement : MonoBehaviour
{

    public GameObject ObjToSpawn;

    private GameObject spwanedObjs;
    private ARRaycastManager raycastManager;
    [SerializeField] VoxelEngine engine;
    Vector2 touchPos;
    public Text debugText;
    public GameObject GameCanvas;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARPlaneManager aRPlaneManager;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        GameCanvas.SetActive(false);
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
                spwanedObjs = Instantiate(ObjToSpawn, hitPose.position, hitPose.rotation);
                if (engine)
                {
                    debugText.text = "in the if";
                    engine.StartGeneration(hitPose.position, hitPose.position.x, hitPose.position.z);
                    //engine.StartGeneration(hitPose.position, spwanedObjs.transform.localScale.x, spwanedObjs.transform.localScale.z);
                }   
                
                foreach (var plane in aRPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                    aRPlaneManager.enabled = !aRPlaneManager.enabled;
                }
                GameCanvas.SetActive(true);
            }
        }
    }
}
