using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    // All game state changes should happen here

    private void Awake()
    {
        // Not checking to see if another instance exists because if we switch scenes
        // then we'll want our EventManager instance to become the one for the current
        // scene

        if (instance != null)
        {
            Debug.Log("Already a GameManager");
            Destroy(gameObject);
        }

        instance = this;
    }

    // Call this on LeaveRoom.cs when players exits Level
    public void LevelCompleted()
    {
        // Unity Analytics Send Level Complete
        //AnalyticsCollection.LevelComplete(SceneManager.GetActiveScene().buildIndex);

        //Debug.Log($"SceneManager.GetActiveScene().buildIndex : {SceneManager.GetActiveScene().buildIndex}");
        //Debug.Log($"Scene Count: {SceneManager.sceneCountInBuildSettings}");

        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene("MainMenu");
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Call this on Player Death to Reset the Level
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
