using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeModule : MonoBehaviour
{
    public UnityEvent OnCheckCollision = new UnityEvent();

    public static float hackingDistance = 4f;

    [Header("Base Stats")]

    public float moveSpeed = 1f;
    public float projectileSpeed = 1f;
    public float gravityScale = 1f;
    public float jumpSpeed = 2f;
    public float dashDuration = 1f;
    public float dashSpeed = 1f;
    public float accelerationSpeed = 1f;
    public float meleeDamage = 1f;
    [Tooltip("Time between attacks")]
    public float attackDelay = 1.5f;
    [Tooltip("Time between dashes")]
    public float dashDelay = 3f;
    [Tooltip("The amount of time between attacks")]
    public float reloadTime = 1f;

    [Header("Sound & Animation")]

    public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip damageSound;
    public AudioClip healSound;
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
    [Tooltip("If checked, the module will never have an editor")]
    protected bool spawnedFromCode = false;

    //[SerializeField]
    [Tooltip("Does the player start with access to this module's editor?")]
    public bool editableOnStart = false;
    //[SerializeField]
    public Editor editor = null;
    /*[SerializeField]
    protected GameObject editorWindow;
    [SerializeField]
    protected GameObject editorButton;*/
    [Header("Parts of body")]
    public GameObject shootFrom;
    
    [Tooltip("Who's your daddy? (Only applies to spawned modules)")]
    public GameObject father;
    // For use by Code
    [HideInInspector]
    public Rigidbody2D rb;
    public Collider2D damagePart;
    [HideInInspector]
    public Collider2D col;
    [HideInInspector]
    public GameObject go;
    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public bool disableOnStart = false;
    // [HideInInspector]
    public bool hackable = false;
    [HideInInspector]
    public Collider2D lastCollidedWith = null;

    protected virtual void Awake()
    {
        //Debug.Log("In Awake");
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (damagePart == null) {
            damagePart = col;
        }
        go = gameObject;
        anim = GetComponent<Animator>();
        rb.gravityScale = gravityScale;

        /*Debug.Log("EditorController.instance: " + EditorController.instance);
        Debug.Log("window: " + editor.window);
        Debug.Log("button: " + editor.button);*/
        if (!spawnedFromCode && editor != null && editor.window != null && editor.button != null) {
            editor = EditorController.instance.AddWindow(editor.window, editor.button, this);
            ToggleEditing(editableOnStart);
        }
        if (disableOnStart && editor != null && editor.window != null)
        {
            editor.window.SetActive(false);
            editor.button.SetActive(false);
        }
        else
        {
            disableOnStart = true;
        }

        if (hackable)
        {
            EnableHackable();
        } else if (TryGetComponent(out HasHealth helth))
        {
            helth.OnDeath.AddListener(EnableHackable);
        } 

        if (!hackable && editor != null && editor.window != null && editor.window.TryGetComponent(out EditorWindow ew))
        {
            ew.ToggleCanExecute(true);
        }
    }

    public virtual void EnableHackable()
    {
        hackable = true;
        if (editor.window.TryGetComponent(out EditorWindow ew))
        {
            ew.ToggleCanExecute(false);
        }
        StartCoroutine(HackableListener());
    }

    public virtual IEnumerator HackableListener()
    {
        while (hackable)
        {
            if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= hackingDistance)
            {
                if (anim != null) {
                    anim.SetTrigger("Hackable");
                }
                ToggleEditing(true);
            }
            else
            {
                if (anim != null) {
                    anim.SetTrigger("Death");
                }
                ToggleEditing(false);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public virtual void ToggleEditing(bool enabled)
    {
        // TO DO: implement event-oriented canvas/raycast target enabling/disabling
        /*editor.window.SetActive(true);*/
        editableOnStart = enabled;
        if (editor.window.TryGetComponent(out EditorWindow ew))
        {
            ew.ToggleEnabled(enabled);
        }
        editor.button.SetActive(enabled);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        lastCollidedWith = other.collider;
    }

    private void OnCollisionExit2D(Collision2D other) {
        lastCollidedWith = null;
    }

    private void OnCollisionStay2D(Collision2D other) {
        lastCollidedWith = other.collider;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        lastCollidedWith = other;
    }

    private void OnTriggerExit2D(Collider2D other) {
        lastCollidedWith = null;
    }

    public virtual Collider2D FindClosestCollider(Collider2D[] colliders, Transform trans)
    {
        Vector2 min_distance = new Vector2(1000f, 1000f);
        Collider2D closest_collider = null;

        // Find closest collider
        foreach (Collider2D col in colliders)
        {
            //print(col.gameObject);
            if (GameObject.ReferenceEquals(trans.gameObject, col.gameObject))
            {
                continue;
            }

            Vector2 temp_distance = trans.position - col.transform.position;
            if (temp_distance.magnitude < min_distance.magnitude)
            {
                closest_collider = col;
                min_distance = temp_distance;
            }
        }

        return closest_collider;
    }
}
