using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : BaseUnit {

    int moveXOffset = 1;
    int moveYOffset = 1;
    float moveSpeed = 0.5f;

    void Start()
    {
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


    void Move()
    {
        Vector2 newPos = FindNextPosition();
        transform.DOMove(newPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(Move);
        transform.DORotate(newPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear);
    }

}
