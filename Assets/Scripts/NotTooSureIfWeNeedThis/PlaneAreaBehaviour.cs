using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneAreaBehaviour : MonoBehaviour
{

    //public TextMeshPro areaText;
   
    public ARPlane arPlane;
    public Vector2 arPlaneSize;
    public Vector3 arPlaneExt;
    public Vector3 arPlaneCenter;
    public Transform arPlaneTransform;


    private PathGenerator PG;

    private void OnEnable()
    {
        //PG = FindObjectOfType<PathGenerator>();
        arPlane.boundaryChanged += ArPlane_BoundaryChanged;
    }

    private void OnDisable()
    {
        arPlane.boundaryChanged -= ArPlane_BoundaryChanged;
    }
    
    void ArPlane_BoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
    {
        
        arPlaneSize = arPlane.size;
        arPlaneExt = arPlane.extents;
        arPlaneCenter = arPlane.center;
        arPlaneTransform =  arPlane.transform;
       
        
        //areaText.text = CalculatePlaneArea(arPlane).ToString();
    }

    //private float CalculatePlaneArea(ARPlane plane)   // used to calculate the area
    //{
    //    return plane.size.x * plane.size.y;
    //}

    //public void ToggleAreaView()
    //{
    //    if (areaText.enabled)
    //        areaText.enabled = false;
    //    else
    //        areaText.enabled = true;
    //}

    //private void Update()  Restting the rotation to look at camera
    //{
    //    areaText.transform.rotation = Quaternion.LookRotation(areaText.transform.position - Camera.main.transform.position);
    //}


}
