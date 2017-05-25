using UnityEngine;
using System.Collections;

public class SlingShotManager : Singleton<SlingShotManager> {


    public Rubber rubber;

    public GameObject leftStart;
    public GameObject rightStart;

    //the point in the "center" of the 'y' for calculating flight direction
    public GameObject slingCenter;

    //base game Object for shot
    public Shot shot;
    public GameObject shotStartPos;

    //current shot on sling
    public Shot currentShot;

    public float shotSpawnTimer;


   


    void OnShotStreching()
    {


    }


    void GenerateSling()
    {

        Rubber rubberLeft = Instantiate<Rubber>(rubber);
        rubberLeft.Init(leftStart.transform.position, currentShot);

        Rubber rubberRight = Instantiate<Rubber>(rubber);
        rubberRight.Init(rightStart.transform.position, currentShot);


    }

    //called from a Shot obj when fired
    public void OnShotFire()
    {
 

       StartCoroutine("SpawnNextShot");
    }


    IEnumerator SpawnNextShot()
    {


        yield return new WaitForSeconds(shotSpawnTimer);

        SpawnShot();
    }

   public void SpawnShot()
    {

        currentShot = Instantiate<Shot>(shot,shotStartPos.transform.position, shotStartPos.transform.rotation);
        currentShot.startPosition = slingCenter.transform.position;
        GenerateSling();
    }


    public void OnShotDeath()
    {


    }

}
