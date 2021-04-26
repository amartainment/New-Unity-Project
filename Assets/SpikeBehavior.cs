using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip deathSound;
    AudioSource mySource;
    void Start()
    {
        mySource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mySource.PlayOneShot(deathSound);
            Destroy(collision.gameObject);
            StartCoroutine(levelFail());
            
        }
    }

    IEnumerator levelFail()
    {
        yield return new WaitForSeconds(1.5f);
        MyEventSystem.levelLose(3);
    }
}
