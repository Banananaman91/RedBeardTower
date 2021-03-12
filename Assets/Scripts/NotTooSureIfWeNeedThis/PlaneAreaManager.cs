using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneAreaManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Input.touchCount == 1)
                {
                    Debug.Log("Tap detected");
                    Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(raycast, out RaycastHit raycastHit))
                    {
                        Debug.Log("Something Hit : " + raycastHit.collider.gameObject.name);
                        var planeAreaBehaviour = raycastHit.collider.gameObject.GetComponent<PlaneAreaBehaviour>();
                        if (planeAreaBehaviour != null)
                        {
                            //planeAreaBehaviour.ToggleAreaView();
                        }
                    }
                }
            }
        }
    }
}
