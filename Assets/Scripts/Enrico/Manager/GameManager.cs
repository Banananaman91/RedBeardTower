using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives;
    int playerCurrentLives;

    [SerializeField]
    private int coins;

    public int Lives { get => lives; set => lives = value; }
    public int Coins { get => coins; set => coins = value; }
    public int PlayerCurrentLives { get => playerCurrentLives; set => playerCurrentLives = value; }

    public HealthBar playerHealtBar;

    //public GameObject Tower1;

    private void Awake()
    {
        PlayerCurrentLives = Lives;
        playerHealtBar.SetMaxHealth(Lives);
    }

    private void Update()
    {
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
