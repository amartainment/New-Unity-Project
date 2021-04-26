using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winScreen;
    public GameObject loseScreen;
    private void OnEnable()
    {
        MyEventSystem.levelWin += LoadWinScreen;
        MyEventSystem.levelLose += LoadLoseScreen;
    }

    private void OnDisable()
    {
        MyEventSystem.levelWin -= LoadWinScreen;
        MyEventSystem.levelLose -= LoadLoseScreen;
    }
    void Start()
    {
        
    }

    void LoadWinScreen(int i)
    {
        winScreen.SetActive(true);
    }

    void LoadLoseScreen(int i)
    {
        loseScreen.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
