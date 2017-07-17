using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlingShotManager : Singleton<SlingShotManager> {


    public Rubber rubber;

    public GameObject leftStart;
    public Rubber rubberLeft;

    public GameObject rightStart;
    public Rubber rubberRight;

    //the point in the "center" of the 'y' for calculating flight direction
    public GameObject slingCenter;

    //base game Object for shot
    public Shot shot;
    public GameObject shotStartPos;

    //current shot on sling
    public Shot currentShot;

    public float shotSpawnTimer;

    public int baseShotCount = 5;
    private Queue<Shot> shots;



    private void Start()
    {

        shots = new Queue<Shot>();
        for (int i = 0; i < baseShotCount;  i++)
        {
            Shot obj =  Instantiate<Shot>(shot);
            obj.gameObject.SetActive(false);
            shots.Enqueue(obj);
        }

        rubberLeft = Instantiate<Rubber>(rubber);
        rubberLeft.gameObject.SetActive(false);
        rubberRight = Instantiate<Rubber>(rubber);
        rubberRight.gameObject.SetActive(false);
    }



    void OnShotStreching()
    {


    }


    void GenerateSling()
    {

        //Rubber rubberLeft = Instantiate<Rubber>(rubber);

        rubberLeft.Init(leftStart.transform.position, currentShot);
        
        //Rubber rubberRight = Instantiate<Rubber>(rubber);
        rubberRight.Init(rightStart.transform.position, currentShot);


    }

    //called from a Shot obj when fired
    public void OnShotFire()
    {
        rubberLeft.gameObject.SetActive(false);
        rubberRight.gameObject.SetActive(false);

        StartCoroutine("SpawnNextShot");
    }


    IEnumerator SpawnNextShot()
    {


        yield return new WaitForSeconds(shotSpawnTimer);

        SpawnShot();
    }

   public void SpawnShot()
    {

        //currentShot = Instantiate<Shot>(shot,shotStartPos.transform.position, shotStartPos.transform.rotation);
        currentShot = shots.Dequeue();
        currentShot.Init(shotStartPos.transform.position, slingCenter.transform.position);
        currentShot.startPosition = slingCenter.transform.position;
        currentShot.gameObject.SetActive(true);
        GenerateSling();
    }


    public void OnShotDeath(Shot shot)
    {
        shot.gameObject.SetActive(false);
        shots.Enqueue(shot);

    }

}
