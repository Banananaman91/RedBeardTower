using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GameObjectPlacement : MonoBehaviour
{
    [SerializeField]
    private Button placeTowerButton;
    public Text debugText;
    public Image reticleUI;
    private Touch theTouch;

    public GameObject placedGameObject;
    private  Vector3 touchPositon;
   

    private void Awake()
    {
        reticleUI.gameObject.SetActive(false);
    }
    private void Update()
    {

    }

    public void CameraRaycast()
    {
        // TO DO: loop around all touch inputs.. works better.
        reticleUI.gameObject.SetActive(true);

        if (Input.touchCount > 0 )
        {
            theTouch = Input.GetTouch(0);
            if(theTouch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
                {
                    switch (hit.collider.name)
                    {
                        default:
                            break;

                        case "PlayArea(Clone)":

                            debugText.text = "hit THE FKING PLANE";
                            reticleUI.color = Color.red;
                            Instantiate(placedGameObject, hit.point, Quaternion.identity);
                            
                            break;
                    }
                }
                else
                {
                    reticleUI.color = Color.white;
                }
            }
            else
            {
                reticleUI.gameObject.SetActive(false);
            }
        }
       
    }
    private void TouchPositioRaycast()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPositon = Input.GetTouch(0).position;
            Ray ray = Camera.main.ScreenPointToRay(touchPositon);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                switch (hit.collider.name)
                {
                    default:
                        break;

                    case "PlayArea(Clone)":
                        debugText.text = "hit THE FKING PLANE";
                        break;
                }
                //Instantiate(placedGameObject, hit.point, transform.rotation);
            }
        }
    }

    private void MousePositionRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //touchPositon = Input.GetTouch(0).position;    
            // Ray ray = Camera.main.ScreenPointToRay(touchPositon);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                switch (hit.collider.name)
                {
                    default:
                        break;

                    case "PlayArea": debugText.text = "hit THE FKING PLANE"; break;
                }
                // Instantiate(placedGameObject, hit.point, transform.rotation);
            }
        }
    }
}
