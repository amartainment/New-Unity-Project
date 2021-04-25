using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<StoneBehavior> stones;
    public List<Transform> stoneLocations;
    public int weight = 0;
    public int weightPerStone = 2;
    Rigidbody2D playerBody;
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        stones = new List<StoneBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        weight = stones.Count * weightPerStone;
    }

    public void AddStone(StoneBehavior stone)
    {
        stones.Add(stone);
        stone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stone.transform.position = stoneLocations[stones.Count - 1].position;
        FixedJoint2D stoneJoint;
        
        if(stone.GetComponent<FixedJoint2D>()!=null)
        {
            stoneJoint = stone.GetComponent<FixedJoint2D>();
            stoneJoint.connectedBody = playerBody;
        } else
        {
            stoneJoint = stone.gameObject.AddComponent<FixedJoint2D>();
            stoneJoint.autoConfigureConnectedAnchor = true;
            stoneJoint.connectedBody = playerBody;

        }
        

    }

    public void removeStone(Vector2 targetPosition, string dir)
    {
        if (stones.Count > 0)
        {
            //stones[0].BreakConnection(strength,direction);
            stones[stones.Count-1].NewBreakConnection(targetPosition, dir);
            stones.Remove(stones[stones.Count-1]);
            
        }
    }

    public void RemoveThisStone(StoneBehavior stone)
    {
        stone.NewBreakConnection(stone.transform.position, "right");
        stones.Remove(stone);
        
    }

    public void removeAllStones()
    {
        if (stones.Count > 0)
        {
            foreach (StoneBehavior stone in stones)
            {
                //stones.Remove(stone);
                stone.NewBreakConnection(stone.transform.position, "right");
                
            }
        }

        stones = new List<StoneBehavior>();
    }
}
