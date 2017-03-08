using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {




    void OnTriggerEnter2D(Collider2D coll)
    {
        BaseUnit baseUnit = coll.gameObject.GetComponent<BaseUnit>();
        if(baseUnit)
        {
            Debug.Log("hit a base Unit");
            baseUnit.OnDeath();
            Destroy(coll.gameObject);
            

        }


    }


}
