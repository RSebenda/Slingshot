using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : BaseUnit {

    public int moveXOffset = 1;

    public float minYOffset;
    public float maxYOffset;

    public int randomDirectionCap = 30;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 5;
    public Vector2 nextPos;

    public GameObject debugTarget;
    public GameObject scorePopup;
    public float scoreLifetime = 1.0f;
    public Ease rotEase;

    //target "cake" , set if in range
    private Cake targetObject;
    private bool movingToTarget = false;


    void Start()
    {
        debugTarget = Instantiate(debugTarget);
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
            debugTarget.transform.position = this.nextPos;
            transform.DOMove(nextPos, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(Move);
        }
        else
        {
            transform.DOMove(targetObject.transform.position, moveSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(BlowUp);

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


    }

   public override void OnDeath()
    {


        Destroy(debugTarget);
    }

    public void OnDeath(int points)
    {
        AudioManager.Instance.OnBombHit();
        scorePopup = Instantiate(scorePopup);
        ScorePopup sp = scorePopup.gameObject.GetComponent<ScorePopup>();
        sp.Init(transform.position, points);

        Destroy(scorePopup, scoreLifetime);
        Destroy(debugTarget);
    }


}
