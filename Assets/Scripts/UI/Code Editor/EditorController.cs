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

    public static GameObject lastCheckpointEditor;
    static bool waiting = true;
    static List<CodeModule> cms = new List<CodeModule>();

    private void Awake() {
        //Debug.Log("EditorController: " + this);
        instance = this;
        Debug.Log("awoken");
        swapper = GetComponent<CodeEditorSwapper>();
        editor_screen = GetComponent<Canvas>();
        CheckpointManager.CheckpointUpdated.AddListener(CheckPoint);
        CheckpointManager.PlayerKilled.AddListener(Player);
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
        // Debug.Log("setting");
        GameObject window = Instantiate(_window, desktop.transform);
        window.transform.SetAsFirstSibling();
        GameObject button = Instantiate(_button, taskbar.transform);
        if (button.TryGetComponent(out EditorButton eb))
        {
            eb.Init(swapper, window.transform);
        }
        if (window.TryGetComponent(out EditorWindow ew))
        {
            cms.Add(module);
            ew.SetCodeSpaceModules(module, cms.Count - 1);
        }
        return new CodeModule.Editor(window, button);
    }

    void CheckPoint() {
        CheckpointManager.CheckpointUpdated.RemoveListener(CheckPoint);
        CheckpointManager.PlayerKilled.RemoveListener(Player);
        if (lastCheckpointEditor != null) {
            Destroy(lastCheckpointEditor);
        }
        lastCheckpointEditor = gameObject;
        lastCheckpointEditor.SetActive(false);
        Copy(gameObject);
        Debug.Log("finished");
    }

    List<EditorWindow> FilterOutInventory(Transform ew) {
        List<EditorWindow> windows = new List<EditorWindow>(); 
        foreach (Transform item in ew)
        {
            if (item.name != "Inventory Window")
                windows.Add(item.GetComponent<EditorWindow>());
        }
        return windows;
    }

    void Copy(GameObject toCopy) {
        // Debug.Log("checking" + toCopy.name);
        GameObject g = Instantiate(toCopy, transform.parent);
        var ew = g.GetComponent<EditorController>();
        // var oldwindows = desktop.GetComponentsInChildren<EditorWindow>();
        List<EditorWindow> oldwindows = FilterOutInventory(desktop.transform);
        List<EditorWindow> windows = FilterOutInventory(ew.desktop.transform);
        foreach (Transform item in desktop.transform)
        {
            if (item.name != "Inventory Window")
                oldwindows.Add(item.GetComponent<EditorWindow>());
        }
        var buttons = ew.taskbar.GetComponentsInChildren<EditorButton>(true);
        // Debug.Log(buttons.Length);
        // Debug.Log(cms.Count);
        for (int i = 0; i < windows.Count; i++)
        {
            var module_id = oldwindows[i].GetID();
            // not set is inventory window
            if (module_id != -1) {
                Debug.Log(oldwindows[i].name);
                Debug.Log(oldwindows[i].GetInstanceID());
                Debug.Log(windows[i].name);
                Debug.Log(windows[i].GetInstanceID());
                // Debug.Log(module_id);
                windows[i].SetCodeSpaceModules(cms[module_id], module_id);
                // Debug.Log(cms[module_id].name);
                buttons[module_id + 1].Init(g.GetComponent<CodeEditorSwapper>(), windows[i].transform);
                buttons[module_id + 1].SetModule(cms[module_id]);
            }
        }
        g.SetActive(true);
        buttons[1].SelectButton();
    }
    void Player() {
        CheckpointManager.CheckpointUpdated.RemoveListener(CheckPoint);
        CheckpointManager.PlayerKilled.RemoveListener(Player);
        Copy(lastCheckpointEditor);
        Destroy(gameObject);

    }
}
