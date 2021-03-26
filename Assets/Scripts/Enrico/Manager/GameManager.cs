using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives;
    int playerCurrentLives;

    private int coins;
    [SerializeField]
    public Text cointText;

    public int Lives { get => lives; set => lives = value; }
    public int Coins { get => coins; set => coins = value; }
    public int PlayerCurrentLives { get => playerCurrentLives; set => playerCurrentLives = value; }

    public HealthBar playerHealtBar;

    public int basicTowerCost = 10;

   // public GameObject[] waypoints;
   
    private void Awake()
    {
        Coins = 100;
        PlayerCurrentLives = Lives;
        playerHealtBar.SetMaxHealth(Lives);

        //for (int i = 0; i < waypoints.Length; i++)
        //{
        //    Vector3 randomPos = new Vector3(UnityEngine.Random.Range(0.2f, -0.2f), 0, UnityEngine.Random.Range(.49f, -.3f));
        //    waypoints[i].transform.position = randomPos;
        //    Instantiate(waypoints[i], waypoints[i].transform.position, waypoints[i].transform.rotation);
        //}
    }
  
    private void Update()
    {
        cointText.text = Coins.ToString();
        checkLife();
       
    }

    private void checkLife()
    {
        if(Lives == 0)
        {
            //GAME OVER BITCH!
        }
    }


}
