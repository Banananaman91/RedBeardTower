using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class GameObjectPlacement : MonoBehaviour
{
    [SerializeField]
    private Button placeTowerButton;
    public Text debugText;
    public Image reticleUI;
    private Touch theTouch;

    //public GameObject placedGameObject;
    private  Vector3 touchPositon;
    private GameManager GM;
    bool towerPlaced = false;
    bool showRadius = false;
    public GameObject ChooseTowerUiPanel;
    private  GameObject prefabReference;
    GameObject t;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        reticleUI.gameObject.SetActive(false);
        ChooseTowerUiPanel.SetActive(false);
    }
   
    private void Update()
    {
        
        if (towerPlaced)
        {
            CameraRaycastOnUpdate();
        }
        
        if (showRadius)
        {
            ShowTowerRadius();
        }

    }


    public void CameraRaycastOnUpdate()
    {
        // TO DO: loop around all touch inputs.. works better.
       // reticleUI.gameObject.SetActive(true);
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.name == "PlayArea(Clone)")
            {
               // reticleUI.color = Color.green;

                if (Input.touchCount > 0)
                {
                    theTouch = Input.GetTouch(0);
                    if(theTouch.phase == TouchPhase.Began)
                    {
                       // fk knows!
                    }
                    if (theTouch.phase == TouchPhase.Ended)
                    {
                        if (GM.Coins >= GM.basicTowerCost)
                        {
                            Instantiate(prefabReference, hit.point, Quaternion.identity);
                            Time.timeScale = 1;
                            GM.Coins -= GM.basicTowerCost;
                            reticleUI.gameObject.SetActive(false);
                            towerPlaced = false;
                            showRadius = false;
                        }
                    }
                }
            }
            else
            {
               // reticleUI.color = Color.red;
            }
        }
    }

    private void ShowTowerRadius()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            debugText.text = hit.collider.name;
            if (hit.collider.name == "PlayArea(Clone)")
            {
                reticleUI.color = Color.green;

            }
            else 
            {
                reticleUI.color = Color.red; 
            }

            if (hit.collider.name == "Pivot" )
            {
                 t = hit.transform.GetChild(1).gameObject;
                 t.SetActive(true);
            }
            else 
            {
                t.SetActive(false);
            }
        }
    }
   
    public void PlaceTower(string prefabToLoad)
    {
        ChooseTowerUiPanel.SetActive(false);
        if(towerPlaced == false)
        {
            towerPlaced = true;
        }
        prefabReference = Resources.Load("Prefabs/Towers/" + prefabToLoad) as GameObject;
    }

    public void PlaceTowerButton()
    {
        showRadius = true;
        ChooseTowerUiPanel.SetActive(true);
        reticleUI.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    private void CameraRaycast()
    {
        // TO DO: loop around all touch inputs.. works better.
        reticleUI.gameObject.SetActive(true);

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Ended)
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
                            //Instantiate(placedGameObject, hit.point, Quaternion.identity);

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
