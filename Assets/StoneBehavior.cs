using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    FixedJoint2D myJoint;
    float gravity;
    float buoyancy;
    public float weight;
    PhysicsManager PhyMan;
    public bool connected = false;
    float verticalVelocity = 0;
    float horizontalVelocity = 0;
    public float stoneThrowMultiplier = 2000;
    public float drag = 1;
    public float timeToTarget = 1f;
    Rigidbody2D myRb;
    public string direction = "right";
    public Vector2 targetPosition;
    public StoneManager myManager;
    Transform playerTrans;
    public bool pickedUp = false;
    public PolygonCollider2D myCollider;
    bool active = true;
    
    void Start()
    {
        PhyMan = GameObject.FindGameObjectWithTag("PhysicsManager").GetComponent<PhysicsManager>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myJoint = GetComponent<FixedJoint2D>();
        gravity = PhyMan.gravity;
        buoyancy = PhyMan.buoyancy;
        myRb = GetComponent<Rigidbody2D>();
        setStoneManager();
    }

    public void setStoneManager()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!connected)
        {
            FallToGround();
            WaterDrag();
        }

        AdjustColliderStatus();
        
    }

   

    public void WaterDrag()
    {
        /*
        if (horizontalVelocity > 0)
        {
            horizontalVelocity -= drag;
        }
        else if(horizontalVelocity <0)
        {
            horizontalVelocity += drag;
        }*/
        if (direction == "right")
        {
            if (transform.position.x >= targetPosition.x)
            {
               
                horizontalVelocity = 0;
                // myRb.velocity = new Vector2(0, myRb.velocity.y);
            }
        } else
        {
            if(transform.position.x <= targetPosition.x )
            {
                horizontalVelocity = 0;
                Debug.Log(transform.position.x);
            }
        }
        
    }

    public void AdjustColliderStatus()
    {
        if(pickedUp)
        {
            myCollider.isTrigger = true;
        } 

        if(!connected && pickedUp)
        {
            myCollider.isTrigger = false;
        }
    }

    public void NewBreakConnection (Vector2 finalPosition, string dir)
    {
        myJoint = GetComponent<FixedJoint2D>();
        Destroy(myJoint);
        Vector3 verticalOffset = new Vector3(0, -5.5f,0);
        transform.position = verticalOffset + playerTrans.position;   
        horizontalVelocity = (finalPosition.x - myRb.position.x) / timeToTarget;
        
        targetPosition = finalPosition;
        Debug.Log("received target " + targetPosition);
        direction = dir;
        connected = false;

        
    }
    
    void FallToGround()
    {
        verticalVelocity = gravity - weight + buoyancy;
        Vector2 targetVelocity = new Vector2(horizontalVelocity, verticalVelocity);
       myRb.velocity = Vector2.Lerp(myRb.velocity, targetVelocity, 10 * Time.deltaTime);
      // myRb.velocity = targetVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active)
        {
            if (collision.collider.CompareTag("Player"))
            {
                if (myManager.stones.Count < 4)
                {
                    myManager.AddStone(this);
                    pickedUp = true;
                    connected = true;
                }

            }
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("endzone"))
        {
            active = false;
        }
    }
}
