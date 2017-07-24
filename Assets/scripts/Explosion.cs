using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


    private float lifeTime = 1.0f;
	// Use this for initialization
	void Start () {

        AudioManager.Instance.OnCakeHit();


        Destroy(this.gameObject, lifeTime);
	}
	
	
}
