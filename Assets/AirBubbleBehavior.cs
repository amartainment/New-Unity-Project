using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubbleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator myAnim;
    public bool used = false;
    public AudioClip bubblePop;
    AudioSource mySource;
    void Start()
    {
        mySource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void Pop()
    {
        used = true;
        myAnim.Play("BubblePop");
        mySource.PlayOneShot(bubblePop);
    }

    
}
