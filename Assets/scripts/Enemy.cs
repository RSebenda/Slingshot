using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : BaseUnit {

    public int moveXOffset = 1;
    public int moveYOffset = 1;
    public int randomDirectionCap = 30;
    public float moveSpeed = 0.5f;
    
    public Vector2 nextPos;

    public GameObject debugTarget;


    void Start()
    {
        debugTarget = Instantiate(debugTarget);
        nextPos = Vector2.down;
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

    void FindNextDirection()
    {
        float randomRot = Random.Range(-randomDirectionCap, randomDirectionCap) + transform.position.z;
        // transform.rotation = Quaternion.Euler(0, 0, randomRot);

        Quaternion nextDirection = Quaternion.Euler(0, 0, randomRot);
        transform.DORotate(new Vector3(0,0, randomRot), 20).SetSpeedBased(true);
        Vector3 pos = transform.position;
        this.nextPos =  pos + nextDirection.eulerAngles;
       
    }


    void Move()
    {
        //this.nextPos = FindNextPosition();
        FindNextDirection();
        debugTarget.transform.position = this.nextPos;
        transform.DOMove(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(Move);

        //transform.DORotate(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.InSine);
    }


   public override void OnDeath()
    {


        Destroy(debugTarget);
    }



}
