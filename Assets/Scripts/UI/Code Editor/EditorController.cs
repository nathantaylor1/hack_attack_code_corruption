using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{
    public static EditorController instance;
    [SerializeField]
    protected GameObject desktop;
    [SerializeField]
    protected GameObject taskbar;

    public bool is_in_editor = false;
    [SerializeField]
    protected Canvas inGameUI;

    protected CodeEditorSwapper swapper;
    public Canvas editor_screen;

    private void Awake() {
        //Debug.Log("EditorController: " + this);
        instance = this;
        swapper = GetComponent<CodeEditorSwapper>();
        editor_screen = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!EditorController.instance.is_in_editor && Input.GetKeyDown(KeyCode.E))
        {
            AnalyticsCollection.OpenedEditor(); // Do Not Delete
            toggleCanvases(true);
            is_in_editor = true;
            //Cursor.visible = true;
            //AudioController.instance.PlayOpen();
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
            //Cursor.visible = false;
            //AudioController.instance.PlayOpen();
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
        //editorParent.SetActive(enabled);
        inGameUI.enabled = !enabled;
        GetComponent<Canvas>().enabled = enabled;
        EventManager.OnToggleEditor?.Invoke(enabled);
    }

    public CodeModule.Editor AddWindow(GameObject _window, GameObject _button, CodeModule module)
    {
        /*Debug.Log("window: " + _window);
        Debug.Log("desktop: " + desktop);*/
        GameObject window = Instantiate(_window, desktop.transform);
        GameObject button = Instantiate(_button, taskbar.transform);
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

    public GameObject CreateLoneWindow(GameObject _window)
    {
        return Instantiate(_window, desktop.transform);
    }

    public CodeModule.Editor AddWindowCopyless(GameObject _window, GameObject _button, CodeModule module)
    {
        /*Debug.Log("window: " + _window);
        Debug.Log("desktop: " + desktop);*/
        _window.transform.parent = desktop.transform;
        //_button.transform.parent = taskbar.transform;
        if (_button.TryGetComponent(out EditorButton eb))
        {
            eb.Init(swapper, _window.transform);
        }
        if (_window.TryGetComponent(out EditorWindow ew))
        {
            ew.SetCodeSpaceModules(module);
        }
        return new CodeModule.Editor(_window, _button);
    }
}
