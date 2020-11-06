using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    static bool isPaused = false ;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public static void PauseOn()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
    public static void PauseOff()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}
