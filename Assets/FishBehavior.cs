using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trans1;
    public Transform trans2;
    Vector2 pos1;
    Vector2 pos2;
    List<Vector2> targetPositions;
    public float speed = 2;
    public bool stunned= false;
    Vector2 targetPosition;
    public int stunTime = 8;
    
    Rigidbody2D myRb;
    bool coroutRunning = false;
    bool stunnedCoroutRunning = false;
    public float time = 0;
    public float duration = 3;
    public Transform spriteTransform;
    int targetIndex = 0;
    Transform myTrans;
    public Animator myAnimator;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        pos1 = trans1.position;
        pos2 = trans2.position;
        
        myTrans = myRb.transform;
        targetPositions = new List<Vector2>();
        targetPositions.Add(pos1);
        targetPositions.Add(pos2);
        PickATarget(); 
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTaget();
        UpdateTarget();
        HandleAnimations();  
    }

    void HandleAnimations()
    {
        if(myRb.velocity.x <0)
        {
            spriteTransform.right = Vector2.right;
        } else
        {
            spriteTransform.right = Vector2.left;
        }
    }

    void MoveToTaget()
    {
        GoToPosition(targetPosition);

    }

    void PickATarget()
    {
        if (targetIndex == 0)
        { targetIndex = 1; }
        else
        {
            targetIndex = 0;
        }
          

        targetPosition = targetPositions[targetIndex];
       
    }

    void GoToPosition(Vector2 position)
    {
        if (!stunned)
        {
            Vector2 direction = position - myRb.position;

            if (direction.sqrMagnitude > 0.1f)
            {
                myRb.velocity = direction.normalized * speed;
            }
            else
            {
                myRb.velocity = Vector2.zero;
                PickATarget();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("stone"))
        {
            StoneBehavior stone = collision.gameObject.GetComponent<StoneBehavior>();
            if (!stone.connected)
            {
                if (!stunnedCoroutRunning)
                {
                    StartCoroutine(StunDelay());
                }
            }
        }

        
        if (collision.CompareTag("Player"))
        {
            if (!stunned)
            {
                myAnimator.Play("chomp");
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("stone"))
        {
            if (!stunnedCoroutRunning)
            {
                StartCoroutine(StunDelay());
            }
        }

        if(collision.collider.CompareTag("Player"))
        {
            myAnimator.Play("chomp");
            Destroy(collision.collider.gameObject);
        }
    }

    void UpdateTarget()
    {   
    }

    public void levelFail()
    {
        MyEventSystem.levelLose(2);
    }

    IEnumerator StunDelay()
    {
        stunned = true;
        Vector2 currentVelocity = myRb.velocity;
        myAnimator.Play("sharkhit");
        stunnedCoroutRunning = true;
        myRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        myRb.velocity = currentVelocity;
        myAnimator.Play("move");
   
        stunned = false;
        
    }
    

 



}
