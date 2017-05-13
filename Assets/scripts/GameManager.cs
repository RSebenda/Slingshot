using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool gameOver = false;
    //cakes list, game over is 0
    public List<Cake> cakes;


    public GameObject baseShot;

    public GameObject baseEnemy;
    public float minSpawnTime;
    public float maxSpawnTime;

    //1/2 width of the screen so you can spawn between -x to x
    public float enemySpawnXWidth;

    // just offscreen of camera
    public float enemySpawnY;

    void Start()
    {
        cakes = new List<Cake>();
        SlingShotManager.Instance.SpawnShot();
        StartCoroutine("SpawnEnemies");
    }




    public void SpawnShot()
    {

        Instantiate(baseShot);

    }


    IEnumerator SpawnEnemies()
    {

        while (!gameOver)
        {
            Vector3 spawnPos = Vector3.zero;
 
            GameObject go = Instantiate(baseEnemy);
            go.transform.position = GenerateSpawnPosition();
            //go.transform.position = GenerateSpawnPosition();
            float enemySpawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(enemySpawnTimer);

        }
    }


   
    public Vector3 GenerateSpawnPosition()
    {
        Vector3 pos = new Vector3();

        pos.x = Random.Range(-enemySpawnXWidth, enemySpawnXWidth);
        pos.y = enemySpawnY;


        return pos;
    }




    public void GameOver()
    {
        gameOver = true;
        StopAllCoroutines();
        Time.timeScale = 0;
        CheckHighScore();

    }

    public bool CheckHighScore()
    {
        int hs = PlayerPrefs.GetInt("highScore");

        if(ScoreManager.Instance.Score > hs)
        {
            PlayerPrefs.SetInt("highScore", ScoreManager.Instance.Score);
            return true;
        }

        return false;
    }

    public void AddCake(Cake cake)
    {
        cakes.Add(cake);

    }

    public void RemoveCake(Cake cake)
    {
        cakes.Remove(cake);
        if(cakes.Count == 0)
        {
            GameOver();

        }


    }


}
