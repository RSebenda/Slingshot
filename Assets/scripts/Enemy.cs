﻿using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : BaseUnit {

    public int moveXOffset = 1;

    public float minYOffset;
    public float maxYOffset;

    public int randomDirectionCap = 30;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 1;
    public Vector2 nextPos;

   
    public GameObject scorePopup;
    public float scoreLifetime = 1.0f;
    public Ease rotEase;

    //target "cake" , set if in range
    private Cake targetObject;
    private bool movingToTarget = false;


    void Start()
    {
        
        nextPos = Vector2.down;
        Move();
        
    }

 


 

    void FindNextDirection()
    {
        //random how far left/right bomb will move 
        float randomRot = Random.Range(-randomDirectionCap, randomDirectionCap) + transform.position.z;
        Vector3 nextDirection = Quaternion.Euler(0, 0, randomRot) * Vector3.down;
        //Debug.Log(nextDirection);

        //random how far down the bomb with fly
        float randomY = Random.Range(minYOffset, maxYOffset);
        this.nextPos = (nextDirection * randomY) + transform.position;

        //keep bombs away frm edges to be fair to player
        Vector3 pos = Camera.main.WorldToViewportPoint(nextPos);

        if(pos.x < 0.1f || pos.x > 0.9f)
        {
            pos.x = (pos.x + 0.5f) / 2;
           
            this.nextPos = Camera.main.ViewportToWorldPoint(pos);
        }


        transform.DORotate(new Vector3(0,0, randomRot), rotationSpeed).SetEase(rotEase);

       
    }


    void Move()
    {
        //if free falling
        if (!movingToTarget)
        {
            FindNextDirection();
            
            transform.DOMove(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(Move);
        }
        else
        {
            if (targetObject)
            {
                transform.DOMove(targetObject.transform.position, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(BlowUp);

                Vector3 rotTo =   transform.position - targetObject.transform.position;

                float rotz = Mathf.Atan2(rotTo.y, rotTo.x) * Mathf.Rad2Deg;
                transform.DOLocalRotate(new Vector3(0,0, rotz - 90), rotationSpeed);
            }
            else
            {
                movingToTarget = false;
                Move();
            }
        }

    }




    public void OnNearbyTarget(Cake cake)
    {

        if (targetObject == null)
        {
            targetObject = cake;
            movingToTarget = true;
        }

    }
    private void BlowUp()
    {
        if (targetObject)
        {
            Cake cake = targetObject.GetComponent<Cake>();
            if (cake)
            {
                cake.OnDeath();

            }

        }
        Destroy(this.gameObject);
    }

   public override void OnDeath()
    {


        
    }

    public void OnDeath(int points)
    {
        AudioManager.Instance.OnBombHit();
        scorePopup = Instantiate(scorePopup);
        ScorePopup sp = scorePopup.gameObject.GetComponentInChildren<ScorePopup>();
        scorePopup.transform.position = this.transform.position;
        sp.Init(transform.position, points);

        Destroy(scorePopup, scoreLifetime);
        
    }


}
