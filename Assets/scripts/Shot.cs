using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Shot : BaseUnit {


    public enum BallState { AtRest, Streching, InFlight };

    public Rigidbody2D rb;
    public Collider2D coll;
    public Renderer rend;

    public float forceMultiplier;

    //fake offset so shot fires in a realistic direction
    public Vector2 startPosition;
    private Vector2 strechPosition;
    public BallState state = BallState.AtRest;

    public int idleLayer;
    public int detectLayer;
    public float heightCap = 1.2f;

    private int hitMultiplier = 1;
    public int baseScore;

    public float rayDistance = 40.0f;

	// Use this for initialization
	void Start ()
    {
        rb.Sleep();
        rb.isKinematic = true;
        // coll.enabled = false;
        //startPosition = transform.position;
        rend = GetComponent<Renderer>();
        transform.DOScale(1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

       
        if (state == BallState.Streching)
        {
            //var touchPos = Input.GetTouch(0).position;
            var mousePos = Input.mousePosition;
            //mousePos.z = 0.0f;
            //strechPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(Physics.Raycast (ray, out hit, rayDistance))
            {
                //Debug.Log(string.Format("mouse click at {0} ", hit.point));
                //Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("HitWall"))
                {
                
                    //Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                    strechPosition = hit.point;
                    transform.position = strechPosition;
             

                }
            }


            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                OnMouseUp();
            }



        }

        //check if offScreen , but give a bit of room on the top

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (state == BallState.InFlight &&  !rend.isVisible && !(pos.y > 1) || pos.y > heightCap)
        {
            //Debug.Log(pos.ToString() + " Off Screen");
            //Destroy(gameObject);
            OnDeath();
            
        }


    }

    public void Init(Vector3 startPosition, Vector3 slingPosition)
    {
        this.transform.position = startPosition;
        this.startPosition = slingPosition;

        //reset all params
        state = BallState.AtRest;
        rb.Sleep();
        rb.isKinematic = true;
        this.gameObject.layer = idleLayer;
        hitMultiplier = 1;
    }


    void OnMouseOver()
    {
        if (state == BallState.AtRest)
        {
            //Debug.Log(string.Format("mouse click at {0} ", Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            AudioManager.Instance.OnSlingStretch();
            state = BallState.Streching;
            
        }
    }

    
    
    void OnMouseUp()
    {

        //enter inflight state/shot fired
        if (state == BallState.Streching)
        {
            AudioManager.Instance.OnShotFire();
            this.gameObject.layer = detectLayer;
            state = BallState.InFlight;
            rb.isKinematic = false;
            rb.WakeUp();
            rb.velocity = Vector2.zero;

            //test flight
            

            Vector2 flightDirection = startPosition - new Vector2(transform.position.x, transform.position.y);
            rb.AddForce(flightDirection * forceMultiplier, ForceMode2D.Impulse);

            //Debug.Log( string.Format("Shooting ball in {0} direction" , flightDirection.ToString() ));

            SlingShotManager.Instance.OnShotFire();

        }
    }



    public override void OnDeath()
    {

        SlingShotManager.Instance.OnShotDeath(this);

    }


    void OnTriggerEnter2D(Collider2D coll)
    {

        //if I hit an enemy,
        //gain score and increasement score Multiplier 
        Enemy enemy = coll.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            Debug.Log("hit an Enemy");
            enemy.OnDeath(baseScore * hitMultiplier);
            //Destroy(coll.gameObject);
            enemy.gameObject.SetActive(false);
            GameManager.Instance.enemies.Enqueue(enemy);

            //add score
            ScoreManager.Instance.AddScore(baseScore * hitMultiplier);

            //double score every object hit
            hitMultiplier *= 2;

        }


    }



}
