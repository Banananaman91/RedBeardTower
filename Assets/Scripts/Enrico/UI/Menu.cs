using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    //public GameObject introPanel;
    private bool isIntroPanleActive = true;
    

   

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            //
        }
    }

   

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
