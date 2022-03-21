using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{
    public static EditorController instance;
    public bool is_in_editor = false;

    //[Tooltip("Editor inventory that should pop up")] 
    public Canvas editor_screen;
    public Canvas in_game_ui;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!EditorController.instance.is_in_editor && Input.GetKeyDown(KeyCode.E))
        {
            AnalyticsCollection.OpenedEditor(); // Do Not Delete
            toggleCanvases(true);
            is_in_editor = true;
            AudioController.instance.PlayOpen();
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
            AnalyticsCollection.ClosedEditor(); // Do Not Delete
            Time.timeScale = 1f;
            AudioController.instance.PlayOpen();
            toggleCanvases(false);
            is_in_editor = false;
        }
    }

    void toggleCanvases(bool enabled) {
        editor_screen.enabled = enabled;
        foreach (var item in editor_screen.GetComponentsInChildren<GraphicRaycaster>())
        {
            item.enabled = enabled;
        }
        in_game_ui.enabled = !enabled;
    }
}
