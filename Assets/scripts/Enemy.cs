using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : BaseUnit {

    public int moveXOffset = 1;
    public int moveYOffset = 1;
    public float moveSpeed = 0.5f;
    
    public Vector2 nextPos;

    public GameObject debugTarget;


    void Start()
    {
        debugTarget = Instantiate(debugTarget);

        Move();
        
    }

    void Update()
    {
        Debug.Log(transform.forward.ToString());

    }



    Vector2 FindNextPosition()
    {
        float randomX = Random.Range(transform.position.x + -moveXOffset, transform.position.x + moveXOffset); 
        Vector2 newPos = new Vector2(randomX, transform.position.y - moveYOffset);
        return newPos;
    }


    void Move()
    {
        this.nextPos = FindNextPosition();
        debugTarget.transform.position = this.nextPos;
        transform.DOMove(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(Move);

        transform.DOLookAt(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.InSine);
    }


   public override void OnDeath()
    {


        Destroy(debugTarget);
    }



}
