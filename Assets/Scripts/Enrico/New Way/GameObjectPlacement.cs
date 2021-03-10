using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectPlacement : MonoBehaviour
{
    [SerializeField]
    private Button placeTowerButton;

    public GameObject placedGameObject;
    private  Vector3 touchPositon;

    private void Awake()
    {
        //placeTowerButton.onClick.AddListener(()=> changePrefabToPlace("Tower"));
    }

    //private void changePrefabToPlace(string prefabName)
    //{
    //    placedGameObject
    //    //placedGameObject = Resources.Load<GameObject>($"Prefabs/Towers/{prefabName}");

    //    if(placedGameObject == null)
    //    {
    //        Debug.Log("Check prefab path and name");
    //    }
    //}

    //bool TryGetTOuchPosition(out Vector2 touchPositon)
    //{
    //    if(Input.touchCount > 0)
    //    {
    //        touchPositon = Input.GetTouch(0).position;
    //    }
    //    touchPositon = default;
    //    return false;
    //}

    private void Update()
    {
        //if(!TryGetTOuchPosition( out Vector2 touchPositon))
        //{
        //    return;
        //}
        if (Input.touchCount > 0)
        {
            touchPositon = Input.GetTouch(0).position;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPositon);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Instantiate(placedGameObject, hit.point, transform.rotation);
            }
        }
    }
}
