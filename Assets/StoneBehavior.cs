﻿using System.Collections;
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
    public float timeToTarget = 1f;
    Rigidbody2D myRb;
    public string direction = "right";
    public Vector2 targetPosition;
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
            }
        }
        
    }

 

    public void NewBreakConnection (Vector2 finalPosition, string dir)
    {
        Destroy(myJoint);
        
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
    }
}
