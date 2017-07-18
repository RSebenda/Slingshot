using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool gameOver = false;
    //cakes list, game over is 0
    public int cakeCount;


    public GameObject baseShot;

    public Enemy baseEnemy;
    public float minSpawnTime;
    public float maxSpawnTime;

    //1/2 width of the screen so you can spawn between -x to x
    public float enemySpawnXWidth;

    // just offscreen of camera
    public float enemySpawnY;

    public Queue<Enemy> enemies;
    public int baseEnemyCount = 20;

    void Start()
    {

        enemies = new Queue<Enemy>();

        for(int i = 0; i< baseEnemyCount; i++)
        {
            Enemy go = Instantiate(baseEnemy);
            go.gameObject.SetActive(false);
            enemies.Enqueue(go);
        }

        SlingShotManager.Instance.SpawnShot();
        StartCoroutine("SpawnEnemies");
    }



    //unused?
    public void SpawnShot()
    {

        Instantiate(baseShot);

    }


    IEnumerator SpawnEnemies()
    {

        while (!gameOver)
        {
            Vector3 spawnPos = Vector3.zero;

            //if out of enemies to spawn
            if (enemies.Count == 0)
                PopulateEnemiesQueue();

            //GameObject go = Instantiate(baseEnemy);
            Enemy go = enemies.Dequeue();
            go.transform.position = GenerateSpawnPosition();
            go.gameObject.SetActive(true);
            go.Init();
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
        
        ScoreManager.Instance.DisplayScore();
        Time.timeScale = 0;
    }

    //if queue is empty add more
    private void PopulateEnemiesQueue()
    {
        int more = 10;

        for (int i = 0; i < more; i++)
        {
            Enemy go = Instantiate(baseEnemy);
            go.gameObject.SetActive(false);
            enemies.Enqueue(go);
        }


    }


    public void AddCake(Cake cake)
    {
        cakeCount++;
    }

    public void RemoveCake(Cake cake)
    {
        cakeCount--;

        if (cakeCount == 0)
        {

            GameOver();
            
        }


    }


}
