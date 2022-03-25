using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeModule : MonoBehaviour
{
    /*[System.Serializable]
    public class Stat<T>
    {
        public bool statApplies = true;
        public T stat;

        public Stat() { }

        public Stat(T _stat)
        {
            stat = _stat;
        }

        public Stat(bool _statApplies, T _stat)
        {
            statApplies = _statApplies;
            stat = _stat;
        }
    }*/

    [Header("Base Stats")]

    public float moveSpeed = 1f;
    public float jumpSpeed = 2f;
    [Tooltip("The amount of time between attacks")]
    public float reloadTime = 1f;

    [Header("Sound & Animation")]

    public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    [Tooltip("The name that precedes each of the generic animations; e.g., the player's " +
        "Animation Name is \"Player\", because its animations are named \"Player Run\", " +
        "\"Player Jump\", etc. This allows code blocks to use generic animation names " +
        "when determining what state a module's Animator is in.")]
    public string animationName;

    [System.Serializable]
    public class Editor
    {
        public GameObject window;
        public GameObject button;
        //public GameObject desktopIcon;

        public Editor(GameObject _window, GameObject _button)
        {
            window = _window;
            button = _button;
        }
    }

    [Header("Code Editor Reference")]

    [SerializeField]
    [Tooltip("Does the player start with access to this module's editor?")]
    protected bool editableOnStart = false;
    [SerializeField]
    protected Editor editor;
    /*[SerializeField]
    protected GameObject editorWindow;
    [SerializeField]
    protected GameObject editorButton;*/

    // For use by Code
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Collider2D col;
    [HideInInspector]
    public GameObject go;
    [HideInInspector]
    public Animator anim;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        go = gameObject;
        anim = GetComponent<Animator>();

        /*Debug.Log("EditorController.instance: " + EditorController.instance);
        Debug.Log("window: " + editor.window);
        Debug.Log("button: " + editor.button);*/
        editor = EditorController.instance.AddWindow(editor.window, editor.button, this);
        ToggleEditing(editableOnStart);
    }

    public virtual void ToggleEditing(bool enabled)
    {
        // TO DO: implement event-oriented canvas/raycast target enabling/disabling
        /*editor.window.SetActive(true);*/
        if (editor.window.TryGetComponent(out EditorWindow ew))
        {
            ew.ToggleEnabled(enabled);
        }
        editor.button.SetActive(enabled);
    }
}
