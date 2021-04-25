using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<StoneBehavior> stones;
    public int weight = 8;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeStone(Vector2 targetPosition, string dir)
    {
        if (stones.Count > 0)
        {
            //stones[0].BreakConnection(strength,direction);
            stones[0].NewBreakConnection(targetPosition, dir);
            stones.Remove(stones[0]);
            weight -= 2;
        }
    }

    public void RemoveThisStone(StoneBehavior stone)
    {
        stone.NewBreakConnection(stone.transform.position, "right");
        stones.Remove(stone);
        weight -= 2;
    }

    public void removeAllStones()
    {
        if (stones.Count > 0)
        {
            foreach (StoneBehavior stone in stones)
            {
                //stones.Remove(stone);
                stone.NewBreakConnection(stone.transform.position, "right");
                weight -= 2;
            }
        }
    }
}
