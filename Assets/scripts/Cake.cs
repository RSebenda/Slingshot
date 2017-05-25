using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour {

    public void Start()
    {

        GameManager.Instance.AddCake(this);


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.gameObject.GetComponent<Enemy>();
       
        //if is bomb, make it move towards me!
        if(e)
        {
            e.OnNearbyTarget(this); 

        }

    }


    public void OnDeath()
    {
        GameManager.Instance.RemoveCake(this);
        Destroy(this.gameObject);

    }


}
