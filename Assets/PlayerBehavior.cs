using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float waterLevel;
    public int weight = 1;
    public float horizontalVelocity = 0;
    public float verticalVelocity = 0;
    private float horizontalMultiplier = 5;
    private float swimStrength = 0.5f;
    public float throwStrength = 0;
    public PhysicsManager phyMan;
    public StoneManager myStones;
    float gravity;
    float buoyancy;
    Rigidbody2D myRb;
    Transform myTrans;
    public bool inWater = false;
    public float oxygenLevel = 100;
    public float oxygenDepletion = 0.1f;
    public Slider oxygenSlider;
    public float airPocketOxygen = 10f;
    LineRenderer throwLine;
    
    void Start()
    {
        myTrans = GetComponent<Transform>();
        myRb = GetComponent<Rigidbody2D>();
        gravity = phyMan.gravity;
        buoyancy = phyMan.buoyancy;
        throwLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForces();
        SetVerticalVelocity();
        SetHorizontalVelocity();
        ThrowRock();
        Swim();
        ControlOxygenLevels();
    }

    void ApplyForces()
    {
        //
        Vector2 resultantVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        
        myRb.velocity = Vector2.Lerp(myRb.velocity, resultantVelocity,10* Time.deltaTime);
        
    }

    
    void ControlOxygenLevels()
    {
        oxygenSlider.value = oxygenLevel / 100;
        if (inWater)
        {
            oxygenLevel -= oxygenDepletion;
        }

        if(!inWater)
        {
            oxygenLevel = 100;
        }

        if(oxygenLevel <=0)
        {
            SceneManager.LoadScene(0);
        }
    }


    void ThrowRock()
    {
        Vector2 targetPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(targetPosition);
        Vector2 throwDirection = (worldPosition - myRb.position).normalized;
        Vector2 horizontifiedThrow = new Vector2(throwDirection.x, 0);
        Vector2 localThrowPosition = new Vector2(throwStrength, 0);
        Vector2 worldThrowPosition = new Vector2();
        string direction = "right";
        if (horizontifiedThrow.x > 0)
        {
             worldThrowPosition = new Vector2(transform.position.x + localThrowPosition.x, 0);
            direction = "right";
        } else
        {
             worldThrowPosition = new Vector2(transform.position.x - localThrowPosition.x, 0);
            direction = "left";
        } 

        if (Input.GetMouseButtonDown(0))
        {
            throwStrength = 0;
        }

        if(Input.GetMouseButton(0))
        {
            
            
            throwStrength += 0.05f;
            if (horizontifiedThrow.x > 0)
            {
                throwLine.SetPosition(1, localThrowPosition);
            } else
            {
                throwLine.SetPosition(1, -localThrowPosition);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
           
            Debug.Log("set Target"+ worldThrowPosition + " direction "+direction);
            myStones.removeStone(worldThrowPosition, direction);
            swimStrength += 0.5f;
            weight -= 2 ;
            horizontalMultiplier += 2;
            myTrans.localScale *= 0.8f;
            throwStrength = 0;
            throwLine.SetPosition(1, Vector3.zero);
        }

       
    }

    void Swim()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            myRb.AddForce(Vector2.up * 1000 * swimStrength);
            oxygenLevel -= 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            myRb.AddForce(- Vector2.up * 1000 * swimStrength);
            oxygenLevel -= 1;
        }
    }
  
    void SetVerticalVelocity()
    {
        if (myTrans.position.y < waterLevel)
        {
            verticalVelocity = gravity - weight + buoyancy;
            inWater = true;
        }
        else
        {
            verticalVelocity = gravity;
            inWater = false;
        }
    }

    void SetHorizontalVelocity()
    {
        horizontalVelocity = Input.GetAxis("Horizontal")*horizontalMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("air"))
        {
            oxygenLevel += airPocketOxygen;
            Destroy(collision.gameObject);
        } 
    }
}
