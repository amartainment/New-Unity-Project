using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite openImage;
    public GameObject diamond;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = openImage;
            diamond.SetActive(true);
        }
    }
}
