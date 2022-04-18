using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static event EventHandler PauseMenuOpened;
    public static event EventHandler PauseMenuClosed;
    public static bool opened;
    public KeyCode pauseKey = KeyCode.Escape;
    private Canvas canvas;

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
                PauseMenuOpened?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // pause menu should be closed
                opened = false;
                canvas.enabled = false;
                Time.timeScale = 1;
                PauseMenuClosed?.Invoke(this, EventArgs.Empty);
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
        SceneManager.LoadScene("MainMenu");
    }
}
