using UnityEngine;
using System.Collections;

public class Shot : BaseUnit {


    public enum BallState { AtRest, Streching, InFlight };

    public Rigidbody2D rb;
    public Collider2D coll;
    public Renderer rend;

    public float forceMultiplier;

    public Vector2 startPosition;
    private Vector2 strechPosition;
    public BallState state = BallState.AtRest;

    public int idleLayer;
    public int detectLayer;
    public float heightCap = 1.2f;

    private int hitMultiplier = 1;
    public int baseScore;

	// Use this for initialization
	void Start ()
    {
        rb.Sleep();
        // coll.enabled = false;
        //startPosition = transform.position;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

       
        if (state == BallState.Streching)
        {
           // var touchPos = Input.GetTouch(0).position;
            var mousePos = Input.mousePosition;
            //mousePos.z = 0.0f;
            strechPosition = Camera.main.ScreenToWorldPoint(mousePos);

            this.transform.position = strechPosition;


        }

        //check if offScreen , but give a bit of room on the top

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (state == BallState.InFlight &&  !rend.isVisible && !(pos.y > 1) || pos.y > heightCap)
        {
            //Debug.Log(pos.ToString() + " Off Screen");
           Destroy(gameObject);
        }


    }

    void OnMouseDown()
    {
        if (state == BallState.AtRest)
        {
            Debug.Log(string.Format("mouse click at {0} ", Camera.main.ScreenToWorldPoint(Input.mousePosition)));
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
            rb.velocity = Vector2.zero;

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
