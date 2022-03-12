using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour
{
    bool is_in_editor = false;

    [Tooltip("Editor inventory that should pop up")] public GameObject editor_screen;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(is_in_editor)
            {
                Time.timeScale = 1f;
                editor_screen.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                editor_screen.SetActive(true);
            }
            is_in_editor = !is_in_editor;
        }
    }
}
