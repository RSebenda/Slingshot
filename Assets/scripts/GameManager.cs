using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool gameOver = false;


    public GameObject baseShot;

    public GameObject baseEnemy;
    public float enemySpawnTimer;

    //1/2 width of the screen so you can spawn between -x to x
    public float enemySpawnXWidth;

    // just offscreen of camera
    public float enemySpawnY;

    void Start()
    {
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
            Quaternion quat = new Quaternion();
            GameObject go = Instantiate(baseEnemy, GenerateSpawnPosition(), quat);

            //go.transform.position = GenerateSpawnPosition();

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

}
