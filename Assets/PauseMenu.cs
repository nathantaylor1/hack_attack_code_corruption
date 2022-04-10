using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool opened;
    public KeyCode pauseKey = KeyCode.Escape;
    private Canvas canvas;
    public int mainMenuSceneIndex = 1;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        opened = canvas.enabled;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (!canvas.enabled)
            {
                // pause menu should be opened
                opened = true;
                canvas.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                // pause menu should be closed
                opened = false;
                canvas.enabled = false;
                Time.timeScale = 1;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        // pause menu should be closed
        opened = false;
        canvas.enabled = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
