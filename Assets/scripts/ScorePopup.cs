using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopup : MonoBehaviour {




    public TextMesh tm;
    private float lifeTime = 1.0f;

    public void Update()
    {
        transform.Translate(0, 0.01f, 0);


    }

    public void Init(Vector3 pos, int score)
    {
        this.transform.position = pos;
        tm.text = score.ToString();

        this.gameObject.SetActive(true);
        Invoke("Disable", lifeTime);

    }

    public void Disable()
    {
        this.gameObject.SetActive(false);

    }

}
