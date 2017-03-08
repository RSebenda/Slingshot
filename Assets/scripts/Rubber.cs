using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber : MonoBehaviour {

    //one of the end of the slingshot
    public Vector3 startPos;

    //reference to moving ball
    public Shot shot;

    public LineRenderer lr;

    public void Init(Vector3 start, Shot shot)
    {
        startPos = start;
        this.shot = shot;

    }

	// Update is called once per frame
	void Update () {

        if(shot == null || shot.state == Shot.BallState.InFlight)
        {
            Destroy(this.gameObject);
            return;
        }

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, shot.transform.position);


    }




}
