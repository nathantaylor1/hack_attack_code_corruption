using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour
{
    public static EditorController instance;
    public bool is_in_editor = false;

    [Tooltip("Editor inventory that should pop up")] public GameObject editor_screen;

    private void Awake() {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            editor_screen.SetActive(true);
            is_in_editor = true;
        }
    }

    public void safeToClose() {
        if(is_in_editor)
        {
            Time.timeScale = 1f;
            editor_screen.SetActive(false);
            is_in_editor = false;
        }
    }
}
