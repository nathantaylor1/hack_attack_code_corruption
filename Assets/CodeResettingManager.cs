using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodeResettingManager : MonoBehaviour
{
    public static CodeResettingManager instance;
    public Canvas canvas;
    private int index;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.activeSceneChanged += ChangedActiveScene;
        canvas = GetComponent<Canvas>();
        transform.SetParent(null);
        // Debug.Log("ahhhh");
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator BrieflyShowResetting() {
        canvas.enabled = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        canvas.enabled = false;
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    private void ChangedActiveScene(Scene prev, Scene curr)
    {
        // don't do anything if going to main menu
        // Debug.Log(current.name);
        // Debug.Log(curr.name);
        // Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (curr.buildIndex == 0 || curr.buildIndex == 1 || curr.buildIndex == index) {
            return;
        }
        // Debug.Log("made it");
        StartCoroutine(BrieflyShowResetting());
        // string currentName = current.name;
        // next.

        // if (currentName == null)
        // {
        //     // Scene1 has been removed
        //     currentName = "Replaced";
        // }

        // Debug.Log("Scenes: " + currentName + ", " + next.name);
    }
}
