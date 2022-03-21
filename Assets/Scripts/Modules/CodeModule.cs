using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeModule : MonoBehaviour
{
    [System.Serializable]
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
    }

    [Header("Base Stats")]

    public Stat<float> moveSpeed = new Stat<float>(1f);
    public Stat<float> jumpSpeed = new Stat<float>(1f);
    [Tooltip("The amount of time between attacks")]
    public Stat<float> reloadTime = new Stat<float>(1f);
    [Tooltip("How quickly projectiles move upon emission from this module")]
    public Stat<float> projectileSpeedMultiplier = new Stat<float>(1f);


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
    protected Editor editor;
    /*[SerializeField]
    protected GameObject editorWindow;
    [SerializeField]
    protected GameObject editorButton;*/

    // For use by Code

    public Rigidbody2D rb;
    public GameObject go;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        go = gameObject;

        editor = EditorController.instance.AddWindow(editor.window, editor.button, this);
    }

    public virtual void EnableEditing()
    {
        // TO DO: implement event-oriented canvas/raycast target enabling/disabling
        /*editor.window.SetActive(true);
        editor.button.SetActive(true);*/
    }

    public virtual void DisableEditing()
    {
       /* editor.window.SetActive(false);
        editor.button.SetActive(false);*/
    }
}
