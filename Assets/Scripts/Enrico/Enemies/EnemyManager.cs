using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
   // public GameObject basicEnemy;

    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    public Text waveCountDownText;

    private float countDown = 0f;
    private int waveIndex;
    public static GameObject startPos;
    public static int basicEnemyDamage = 1;
    public static int enemiesAlive = 0;

    private void Update()
    {
        if(startPos != null) { return; }
        startPos = GameObject.FindObjectOfType<StartHolder>().gameObject;
    }

    private void LateUpdate()
    {
        if (enemiesAlive > 0) { return; }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            Spawn(wave.enemy);

            yield return new WaitForSeconds(1f/ wave.rate);
        }
        waveIndex++;
        if (waveIndex == waves.Length) // NEXT LEVEL???
        {
            // OPEN CANVAS TO SHOW HE WON, POOR GUY.
        }
    }

    public void Spawn( GameObject enemy)
    {
        Instantiate(enemy, startPos.transform.position, startPos.transform.rotation);
        enemiesAlive++;
    }
   
}

