using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{
    public static EditorController instance;
    public bool is_in_editor = false;

    //[Tooltip("Editor inventory that should pop up")] 
    private GameObject editor_screen;

    private void Awake() {
        instance = this;
        editor_screen = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!EditorController.instance.is_in_editor && Input.GetKeyDown(KeyCode.E))
        {
            toggleCanvases(true);
            is_in_editor = true;
            Time.timeScale = 0f;
        } 
        else if(EditorController.instance.is_in_editor && Input.GetKeyDown(KeyCode.E))
        {
            SafeClose();
        }
    }

    public void SafeClose() {
        if(is_in_editor)
        {
            Time.timeScale = 1f;
            toggleCanvases(false);
            is_in_editor = false;
        }
    }

    void toggleCanvases(bool enabled) {
        GetComponent<Canvas>().enabled = enabled;
        foreach (var item in GetComponentsInChildren<GraphicRaycaster>())
        {
            item.enabled = enabled;
        }
    }
}
