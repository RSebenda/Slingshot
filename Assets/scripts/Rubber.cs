using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber : MonoBehaviour {

    //one of the end of the slingshot
    public Vector3 startPos;

    //reference to moving ball
    public Shot shot;
    public Color color;
    public LineRenderer lr;

    public void Init(Vector3 start, Shot shot)
    {
        startPos = start;
        this.shot = shot;

        this.gameObject.SetActive(true);
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, shot.transform.position);
        lr.startColor = color;
        lr.endColor = color;
    }

	// Update is called once per frame
	void Update () {

        
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, shot.transform.position);


    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }



}
