using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<StoneBehavior> stones;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeStone(Vector2 targetPosition, string dir)
    {
        //stones[0].BreakConnection(strength,direction);
        stones[0].NewBreakConnection(targetPosition,dir);
        stones.Remove(stones[0]);
    }
}
