using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{
    public static EditorController instance;
    [SerializeField]
    protected static GameObject desktop;
    [SerializeField]
    protected static GameObject toolbar;

    public bool is_in_editor = false;
    [SerializeField]
    protected GameObject editorParent;

    //[Tooltip("Editor inventory that should pop up")] 
    private GameObject editor_screen;
    protected CodeEditorSwapper swapper;

    private void Awake() {
        instance = this;
        editor_screen = gameObject;
        swapper = GetComponent<CodeEditorSwapper>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!EditorController.instance.is_in_editor && Input.GetKeyDown(KeyCode.E))
        {
            AnalyticsCollection.OpenedEditor(); // Do Not Delete
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
            AnalyticsCollection.ClosedEditor(); // Do Not Delete
            Time.timeScale = 1f;
            toggleCanvases(false);
            is_in_editor = false;
        }
    }

    void toggleCanvases(bool enabled) {
        /*GetComponent<Canvas>().enabled = enabled;
        foreach (var item in GetComponentsInChildren<GraphicRaycaster>())
        {
            item.enabled = enabled;
        }*/
        editorParent.SetActive(enabled);
    }

    public CodeModule.Editor AddWindow(GameObject _window, GameObject _button, CodeModule module)
    {
        GameObject window = Instantiate(_window, desktop.transform);
        GameObject button = Instantiate(_button, toolbar.transform);
        if (button.TryGetComponent(out EditorButton eb))
        {
            eb.Init(swapper, window.transform);
        }
        if (window.TryGetComponent(out EditorWindow ew))
        {
            ew.SetCodeSpaceModules(module);
        }
        return new CodeModule.Editor(window, button);
    }
}
