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
        swapper = GetComponent<CodeEditorSwapper>();
        editor_screen = GetComponent<Canvas>();
       CheckpointManager.CheckpointUpdated.AddListener(CheckPoint);
       CheckpointManager.PlayerKilled.AddListener(Player);
    }
    IEnumerator test() {
        yield return new WaitForSeconds(2f);
            CheckPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting) {
            waiting = false;
            StartCoroutine(test());
        }
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
        Debug.Log("setting");
        GameObject window = Instantiate(_window, desktop.transform);
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
        Copy(gameObject);
        if (lastCheckpointEditor != null) {
            Destroy(lastCheckpointEditor);
        }
        lastCheckpointEditor = gameObject;
        gameObject.SetActive(false);
    }

    void Copy(GameObject toCopy) {
        Debug.Log("checking" + toCopy.name);
        GameObject g = Instantiate(toCopy, transform.parent);
        var windows = g.GetComponentsInChildren<EditorWindow>(true);
        var oldwindows = gameObject.GetComponentsInChildren<EditorWindow>(true);
        var buttons = g.GetComponent<EditorController>().taskbar.GetComponentsInChildren<EditorButton>(true);
        Debug.Log(windows.Length);
        Debug.Log(cms.Count);
        for (int i = 1; i < windows.Length; i++)
        {
            var module_id = oldwindows[i].GetID();
            // not set is inventory window
            if (module_id != -1) {
                windows[i].SetCodeSpaceModules(cms[oldwindows[i].GetID()], oldwindows[i].GetID());
                buttons[i].Init(g.GetComponent<CodeEditorSwapper>(), windows[i].transform);
            }
        }
        g.SetActive(true);
    }
    void Player() {
        CheckpointManager.CheckpointUpdated.RemoveListener(CheckPoint);
        CheckpointManager.PlayerKilled.RemoveListener(Player);
        Copy(lastCheckpointEditor);
        Destroy(gameObject);
    }
}
