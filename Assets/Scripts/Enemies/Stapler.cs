using System;
using System.Collections;
using UnityEngine;
public class Stapler : EnemyMovement
{
    public Collider2D col2d;
    public LayerMask playerLayerMask;
    public float cooldownTimeSeconds = 1;

    public Vector2 offset;
    public Vector2 attackArea;
    
    public bool drawAttackAreaGizmo;
    
    private float _cooldownTimer;
    private bool _grounded;
    private bool _canTakeAction;
    private bool _jumping;
    [NonSerialized] public string ID;

    [SerializeField] private bool facingRight;

    public Transform firingPosition;
    public GameObject projectilePrefab;
    public float shootForce = 50f;

    private bool _shooting;
    public LayerMask groundLayer;
    private Rigidbody2D _rb2d;
    public float jumpForce = 1000f;
    private StaplerAnimations _staplerAnimations;
    
    public AudioClip shootSound;
    public AudioClip jumpSound;
    private SpriteRenderer spr;
    
    private void Awake()
    {
        _cooldownTimer = cooldownTimeSeconds;
        _rb2d = GetComponent<Rigidbody2D>();
        _staplerAnimations = GetComponent<StaplerAnimations>();
        spr = GetComponent<SpriteRenderer>();

        ID = gameObject.name + transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!spr.isVisible) return; // do nothing if not visible
        
        _grounded = Grounded.Check(col2d);

        float yVel = _rb2d.velocity.y;
        if (yVel < 0)
        {
            _staplerAnimations.SetFall(true);
        }

        if (!_grounded || _jumping) return;
        
        _staplerAnimations.SetFall(false);
        _staplerAnimations.SetJump(false);
        
        UpdateCooldownTimer();
        if (!_canTakeAction) return;
        
        ShootAction();
        if (!_shooting) JumpAction();

        _shooting = false;
    }

    private void UpdateCooldownTimer()
    {
        _cooldownTimer -= Time.fixedDeltaTime;
        if (_cooldownTimer < 0)
        {
            _canTakeAction = true;
            _cooldownTimer = cooldownTimeSeconds;
        }
        else
        {
            _canTakeAction = false;
        }
    }

    private void ShootAction()
    {
        Collider2D col = Physics2D.OverlapBox(col2d.bounds.center + (Vector3)offset, attackArea, 0f, playerLayerMask);
        if (!col) return;

        if (shootSound != null && AudioManager.instance != null)
            AudioManager.instance.PlaySound(shootSound, transform.position);
        _shooting = true;
        Flip(col);
        
        _staplerAnimations.SetOpen(true);
        StartCoroutine(CO_ShootAnim(col));
    }

    private void Flip(Collider2D target)
    {
        bool prev = facingRight;
        facingRight = (target.transform.position.x - transform.position.x) < 0;
        if (facingRight != prev) {
            Rotate();
        }
    }

    private void Rotate()
    {
        FlipDirection();
        transform.Rotate(Vector3.up, 180);
    }

    private void Shoot(Collider2D target)
    {
        GameObject go = Instantiate(projectilePrefab, firingPosition.position, Quaternion.identity);
        
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.ID = ID;
        
        Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
        Vector3 targetPos = target.transform.position - transform.position;
        targetPos.z = 0;
        targetPos = targetPos.normalized;
        if (facingRight)
        {
            go.transform.Rotate(Vector3.up, 180);
        }
        rb2d.AddForce((targetPos * shootForce));
    }

    private IEnumerator CO_ShootAnim(Collider2D col)
    {
        yield return new WaitForSeconds(0.2f);
        Shoot(col);
        Debug.Log("Shoot Anim");
        yield return new WaitForSeconds(0.1f);
        _staplerAnimations.SetOpen(false);
        yield return null;
    }

    private void JumpAction()
    {
        if (jumpSound != null && AudioManager.instance != null)
            AudioManager.instance.PlaySound(jumpSound, transform.position);
        _staplerAnimations.SetJump(true);
        _jumping = true;
        if (/* Wall Detection: */ Physics2D.Raycast(transform.position, transform.right, 2f, ~groundLayer) 
                                  || /* Edge Detection: */ !Physics2D.Raycast(transform.position, (transform.right + transform.up * -1).normalized, 1.8f, ~groundLayer))
        {
            facingRight = !facingRight;
            Rotate();
        }
        _rb2d.AddForce((transform.right + transform.up * 5).normalized * jumpForce);
        StartCoroutine(CO_Jumping());
    }

    private void OnDrawGizmos()
    {
        if (!drawAttackAreaGizmo) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col2d.bounds.center + (Vector3)offset, attackArea);
        //Gizmos.DrawLine(transform.position, transform.position+(transform.right*2f));
    }

    private IEnumerator CO_Jumping()
    {
        yield return new WaitForSeconds(0.4f);
        _jumping = false;
        yield return null;
    }
}
