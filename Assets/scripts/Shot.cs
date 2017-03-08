using UnityEngine;
using System.Collections;

public class Shot : BaseUnit {


    public enum BallState { AtRest, Streching, InFlight };

    public Rigidbody2D rb;
    public Collider2D coll;

    public float forceMultiplier;

    public Vector2 startPosition;
    private Vector2 strechPosition;
    public BallState state = BallState.AtRest;

    public int idleLayer;
    public int detectLayer;


    private int hitMultiplier = 1;
    public int baseScore;

	// Use this for initialization
	void Start ()
    {
        rb.Sleep();
       // coll.enabled = false;
        //startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if(state == BallState.Streching)
        {

            var mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            strechPosition = Camera.main.ScreenToWorldPoint(mousePos);

            this.transform.position = strechPosition;


        }

       
    }

    void OnMouseDown()
    {
        if (state == BallState.AtRest)
        {
            Debug.Log("---> click: ");
            state = BallState.Streching;
        }
    }

    

    void OnMouseUp()
    {

        //enter inflight state
        if (state == BallState.Streching)
        {
            this.gameObject.layer = detectLayer;
            state = BallState.InFlight;
            rb.WakeUp();

            //test flight
            

            Vector2 flightDirection = startPosition - new Vector2(transform.position.x, transform.position.y);
            rb.AddForce(flightDirection * forceMultiplier, ForceMode2D.Impulse);

            Debug.Log( string.Format("Shooting ball in {0} direction" , flightDirection.ToString() ));

            SlingShotManager.Instance.OnShotFire();

        }
    }



    public override void OnDeath()
    {

        //GameManager.Instance.SpawnShot();

    }


    void OnTriggerEnter2D(Collider2D coll)
    {

        //if I hit an enemy,
        //gain score and increasement score Multiplier 
        Enemy enemy = coll.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("hit an Enemy");
            enemy.OnDeath();
            Destroy(coll.gameObject);

            //add score
            ScoreManager.Instance.AddScore(baseScore * hitMultiplier);

            //double score every object hit
            hitMultiplier *= 2;

        }


    }



}
