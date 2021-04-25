using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trans1;
    public Transform trans2;
    public Transform trans3;
    Vector2 pos1;
    Vector2 pos2;
    Vector2 fleePos;
    public float speed = 2;
    public bool fleeing = false;
    Vector2 targetPosition;
    Vector2 startPosition;
    Rigidbody2D myRb;
    bool coroutRunning = false;
    bool fleeCoroutRunning = false;
    public float time = 0;
    public float duration = 3;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        pos1 = trans1.position;
        pos2 = trans2.position;
        fleePos = trans3.position;
        targetPosition = pos2;
        startPosition = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        UpdateTarget();
    }

    

    void MoveToTarget()
    {
        time += Time.deltaTime;
        float t = time / duration;
        t = t * t * (3f - 2f * t);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    }

    void UpdateTarget()
    {
        if (!fleeing)
        {
            if (Vector2.SqrMagnitude(myRb.position - targetPosition) <= 0.1f)
            {
                if (!coroutRunning)
                    StartCoroutine(changeTarget());
                startPosition = targetPosition;
            }
        } else
        {

        }
    }

    IEnumerator resetFish()
    {
        fleeCoroutRunning = true;
        yield return new WaitForSeconds(5);
        startPosition = transform.position;
        targetPosition = pos1;
        fleeCoroutRunning = false;
    }
    IEnumerator changeTarget()
    {
        coroutRunning = true;
        yield return new WaitForSeconds(2);
        if(targetPosition == pos2)
        {
            targetPosition = pos1;
        } else
        {
            targetPosition = pos2;
        }
        coroutRunning = false;
        time = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("stone"))
        {
            
            coroutRunning = false;
            fleeing = true;
            StartCoroutine(resetFish());
        }
    }




}
