using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void updateText(int i)
    {
        if(i==1)
        {
            text1.SetActive(true);
            text2.SetActive(false);
            text3.SetActive(false);
        }

        if(i==2)
        {
            text2.SetActive(true);
            text1.SetActive(false);
            text3.SetActive(false);
        }

        if(i==3)
        {
            text3.SetActive(true);
            text1.SetActive(false);
            text2.SetActive(false);
        }
    }


}
