using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [HideInInspector]
    public Coroutine hackableCoroutine = null;
    protected CheckpointReset cr = null;

    [SerializeField] private Color dashTrailColor = Color.white;
    [HideInInspector] public TrailRenderer tr;

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
        cr = GetComponent<CheckpointReset>();
        rb.gravityScale = gravityScale;

        /*Debug.Log("EditorController.instance: " + EditorController.instance);
        Debug.Log("window: " + editor.window);
        Debug.Log("button: " + editor.button);*/
        if (!spawnedFromCode && editor != null && editor.window != null && editor.button != null) {
            editor = EditorController.instance.AddWindow(editor.window, editor.button, this);
            ToggleEditing(editableOnStart);
        }
        if (disableOnStart && editor != null && editor.window != null && editor.button != null)
        {
            editor.window.SetActive(false);
            editor.button.SetActive(false);
        }
        else
        {
            disableOnStart = true;
        }

        if (TryGetComponent(out HasHealth helth))
        {
            helth.OnDeath.AddListener(EnableHackable);

            //Debug.Log(gameObject.name + " is hackable? " + hackable);
            if (hackable)
            {
                helth.Damage(helth.maxHealth * 2);
                //cr.MarkForNoReset();
            }
        }

        /*if (hackable)
        {
            EnableHackable();
        } else if (TryGetComponent(out HasHealth helth))
        {
            helth.OnDeath.AddListener(EnableHackable);
        } */

        if (!hackable && editor != null && editor.window != null && editor.window.TryGetComponent(out EditorWindow ew))
        {
            ew.ToggleCanExecute(true);
        }

        StartCoroutine(CO_AddTrailRenderer());
        
        EventManager.OnToggleEditor.AddListener(MarkForReset);
    }

    protected void MarkForReset(bool shouldReset)
    {
        if (shouldReset && editableOnStart)
        {
            cr.MarkForReset();
        }
    }

    private IEnumerator CO_AddTrailRenderer()
    {
        if (gameObject.GetComponent<TrailRenderer>()) yield break;
        
        // Add Trail Renderer for Dash
        tr = gameObject.AddComponent<TrailRenderer>();
        
        // make it not show up -- Dash edits this while dashing
        tr.emitting = false;

        // set remain time
        tr.time = 0.2f;

        // set color
        tr.startColor = dashTrailColor;
        tr.endColor = dashTrailColor;
        
        // width curve
        AnimationCurve crv = new AnimationCurve(
            new Keyframe(0,0.5f, 0, 0), 
            new Keyframe(1, 0,0, 0));
        tr.widthCurve = crv;
        
        // material
        tr.textureMode = LineTextureMode.Tile;
        tr.material = AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat");
        yield return null;
    }

    protected virtual void OnEnable()
    {
        if (hackable)
        {
            hackableCoroutine = StartCoroutine(HackableListener());
        }
    }

    public virtual void EnableHackable()
    {
        //Debug.Log(gameObject.name + " got into EnableHackable() function");
        hackable = true;
        if (editor.window.TryGetComponent(out EditorWindow ew))
        {
            ew.ToggleCanExecute(false);
        }
        hackableCoroutine = StartCoroutine(HackableListener());
    }

    public virtual IEnumerator HackableListener()
    {
        while (hackable)
        {
            if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= hackingDistance)
            {
                //Debug.DrawLine(GameManager.instance.player.transform.position, transform.position);
                if (anim != null) {
                    anim.SetTrigger("Hackable");
                }
                if (!editableOnStart)
                    ToggleEditing(true);
            }
            else
            {
                if (anim != null) {
                    anim.SetTrigger("Death");
                }
                if (editableOnStart)
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
        editor.button.transform.SetAsLastSibling();
        if (editor.button.TryGetComponent(out EditorButton eb))
        {
            eb.SelectButton();
        }
        /*if (enabled)
        {
            cr.MarkForReset();
        }*/
    }

    public virtual void DisableGravity()
    {
        rb.gravityScale = 0;
    }

    public virtual void EnableGravity()
    {
        rb.gravityScale = gravityScale > 0 ? gravityScale : 1;
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
