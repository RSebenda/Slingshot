using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : BaseUnit {

    public int moveXOffset = 1;
    public int moveYOffset = 1;
    public int randomDirectionCap = 30;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 5;
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
   
        Vector3 nextDirection = Quaternion.Euler(0, 0, randomRot) * Vector3.down;
        //Debug.Log(nextDirection);


        this.nextPos = (nextDirection * 2)+ transform.position;
        transform.DORotate(new Vector3(0,0, randomRot), rotationSpeed).SetSpeedBased(true).SetEase(Ease.InOutBounce);

       
    }


    void Move()
    {

        FindNextDirection();
        debugTarget.transform.position = this.nextPos;
        transform.DOMove(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(Move);

    }


   public override void OnDeath()
    {


        Destroy(debugTarget);
    }



}
