using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBehavior : MonoBehaviour
{
    PlayerBehavior player;
    FixedJoint2D myJoint;
    public bool treasureCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerBehavior>();
        myJoint = GetComponent<FixedJoint2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.CompareTag("Player"))
        {
            Vector3 offset = new Vector3(0, -5.5f, 0);
            transform.position = collision.gameObject.transform.position + offset;
            myJoint.connectedBody = collision.attachedRigidbody;
            treasureCollected = true;
            player.treasureCollected = treasureCollected;
        }
    }


}
