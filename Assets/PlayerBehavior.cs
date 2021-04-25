using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float waterLevel;
    public int stoneWeight = 1;
    public float horizontalVelocity = 0;
    public float verticalVelocity = 0;
    private float horizontalMultiplier = 15;
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
    public float swimmingOxygen = 2.5f;
    LineRenderer throwLine;
    public Animator swimAnimator;
    public Transform spriteTransform;
    public float throwDistance = 5f;
    public bool levelStarted = false;
    public GameObject rope;
    public GameObject splashPrefab;
    private bool splashed = false;
    
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
        if (levelStarted)
        {
            
            SetVerticalVelocity();

            ThrowRock();
            Swim();
            ControlOxygenLevels();
            HandleAnimations();
        }

        SetHorizontalVelocity();
        ApplyForces();
        HandleLevelStart();
    }

    void HandleLevelStart()
    {
        if (!levelStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                levelStarted = true;
                rope.SetActive(false);
            }
        }
    }
    void HandleAnimations()
    {
        if(horizontalVelocity >0)
        {
            spriteTransform.up = Vector3.left;
        } else if (horizontalVelocity <0)
        {
            spriteTransform.up = Vector3.right;
        } else
        {
            if (myRb.velocity.y > 0)
            {
                spriteTransform.up = Vector3.down;
            }
            else
            {
                spriteTransform.up = Vector3.up;
            }
        }
    }
    void ApplyForces()
    {
        stoneWeight = myStones.weight;
        int numberOfStones = stoneWeight / myStones.weightPerStone;
        //
        Vector2 resultantVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        
        myRb.velocity = Vector2.Lerp(myRb.velocity, resultantVelocity,20* Time.deltaTime);

        //adjust swim strength based on weight - shameful really, hardcoded values

       
        switch (numberOfStones)
        {
            case 4:
                swimStrength = 0.5f;
                horizontalMultiplier = 9; //was 5
                
                break;
            case 3:
                swimStrength = 1f;
                horizontalMultiplier = 9; // was 7
                break;
            case 2:
                swimStrength = 1.5f;
                horizontalMultiplier = 9;
                break;
            case 1:
                swimStrength = 2f;
                horizontalMultiplier = 15;// was 11
                break;
            case 0:
                swimStrength = 2.5f;
                horizontalMultiplier = 15; // was 13
                break;
            default:
                swimStrength = 2.5f;
                horizontalMultiplier = 15;
                break;
        }
        
    }

    
    void ControlOxygenLevels()
    {
        oxygenSlider.value = oxygenLevel / 100;
        if (inWater)
        {
            
            oxygenLevel -= oxygenDepletion;
            if(!splashed)
            {
                spriteTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                Instantiate(splashPrefab, transform.position, Quaternion.identity);
                splashed = true;
            }
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
             worldThrowPosition =  new Vector2(transform.position.x + throwDistance, 0);
            direction = "right";
        } else
        {
             worldThrowPosition = new Vector2(transform.position.x - throwDistance, 0);
            direction = "left";
        } 

        if (Input.GetMouseButtonDown(0))
        {
           // throwStrength = 0;
            myStones.removeStone(worldThrowPosition, direction);
        }

        if(Input.GetMouseButton(0))
        {
            
            
          //  throwStrength += 0.05f;

/*
            if (horizontifiedThrow.x > 0)
            {
                throwLine.SetPosition(1, localThrowPosition);
            } else
            {
                throwLine.SetPosition(1, -localThrowPosition);
            }
*/
        }

        if(Input.GetMouseButtonUp(0))
        {
           
          //  Debug.Log("set Target"+ worldThrowPosition + " direction "+direction);

          //  myStones.removeStone(worldThrowPosition, direction);
            //swimStrength += 0.5f;
            
           // horizontalMultiplier += 2;
         //   throwStrength = 0;
        //    throwLine.SetPosition(1, Vector3.zero);
        }

       
    }

    void Swim()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            myRb.AddForce(Vector2.up * 1000 * swimStrength);
            oxygenLevel -= swimmingOxygen;
            
            if(!swimAnimator.GetCurrentAnimatorStateInfo(0).IsName("swim"))
            swimAnimator.Play("swim");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            myRb.AddForce(- Vector2.up * 1000 * swimStrength);
            oxygenLevel -= swimmingOxygen;
            if (!swimAnimator.GetCurrentAnimatorStateInfo(0).IsName("swim"))
                swimAnimator.Play("swim");
        }
    }
  
    void SetVerticalVelocity()
    {
        if (myTrans.position.y < waterLevel)
        {
            verticalVelocity = gravity - stoneWeight + buoyancy;
            inWater = true;
        }
        else
        {
            verticalVelocity = 2* gravity;
            inWater = false;
        }
    }

    void SetHorizontalVelocity()
    {
        horizontalVelocity = Input.GetAxis("Horizontal")*horizontalMultiplier;
        if (horizontalVelocity > 0 || horizontalVelocity < 0)
        {
            if (!swimAnimator.GetCurrentAnimatorStateInfo(0).IsName("swim"))
                swimAnimator.Play("swim");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("air"))
        {
            oxygenLevel += airPocketOxygen;
            Destroy(collision.gameObject);
        }
       
       if(collision.CompareTag("endzone"))
        {
            myStones.removeAllStones();
        }
    }
}
