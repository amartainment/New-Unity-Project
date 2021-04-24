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
    bool connected = true;
    float verticalVelocity = 0;
    float horizontalVelocity = 0;
    public float stoneThrowMultiplier = 2000;
    public float drag = 1;
    Rigidbody2D myRb;
    void Start()
    {
        PhyMan = GameObject.FindGameObjectWithTag("PhysicsManager").GetComponent<PhysicsManager>();
        myJoint = GetComponent<FixedJoint2D>();
        gravity = PhyMan.gravity;
        buoyancy = PhyMan.buoyancy;
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!connected)
        {
            FallToGround();
            WaterDrag();
        }

        
    }

    public void WaterDrag()
    {
        if (horizontalVelocity > 0)
        {
            horizontalVelocity -= drag;
        }
        else if(horizontalVelocity <0)
        {
            horizontalVelocity += drag;
        }
        
    }

    public void BreakConnection(float strength, Vector2 direction)
    {
        Destroy(myJoint);
        if(direction.x >= 0)
        {
            horizontalVelocity = strength * stoneThrowMultiplier;
        } else
        {
            horizontalVelocity = strength * stoneThrowMultiplier * -1;
        }
        
        //myRb.AddForce(strength * stoneThrowMultiplier * direction);
        connected = false;
    }
    
    void FallToGround()
    {
        verticalVelocity = gravity - weight + buoyancy;
        Vector2 targetVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        myRb.velocity = Vector2.Lerp(myRb.velocity, targetVelocity, 10 * Time.deltaTime);
    }
}
