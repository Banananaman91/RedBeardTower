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

    private void Awake()
    {
        Coins = 100;
        PlayerCurrentLives = Lives;
        playerHealtBar.SetMaxHealth(Lives);
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
