using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChompAction : MonoBehaviour
{
    public FishBehavior fish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void heroDead()
    {
        fish.levelFail();
    }


}
