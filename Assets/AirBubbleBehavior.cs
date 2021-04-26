using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubbleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator myAnim;
    public bool used = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void Pop()
    {
        used = true;
        myAnim.Play("BubblePop");
    }

    
}
