using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour {

    public GameObject gameUi;
    public GameObject stopUi;
    public static bool GamePaused;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GamePaused)
            {
                Pause();
            }
            else if (GamePaused)
            {
                Resume();
            }
        }

    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameUi.SetActive(true);
        stopUi.SetActive(false);
        //Time.timeScale = 1f;
        GamePaused = false;
        Debug.Log("Game Pause: " + GamePaused + "\nGame resumed.");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameUi.SetActive(false);
        stopUi.SetActive(true);
        //Time.timeScale = 0f;
        GamePaused = true;
        Debug.Log("Game Pause: " + GamePaused + "\nGame paused.");
    }
}
