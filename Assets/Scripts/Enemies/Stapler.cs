using System;
using UnityEngine;
public class Stapler : MonoBehaviour
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

    [SerializeField] private bool facingRight;

    public Transform firingPosition;
    public GameObject projectilePrefab;
    public float shootForce = 50f;

    private bool _shooting;
    public LayerMask groundLayer;
    private Rigidbody2D _rb2d;
    public float jumpForce = 1000f;

    private void Awake()
    {
        _cooldownTimer = cooldownTimeSeconds;
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _grounded = Grounded.Check(col2d);
        if (!_grounded) return;
        
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

        _shooting = true;
        Flip(col);
        Shoot(col);
        
        Debug.Log("Stapler " + gameObject.name + " shoot");
    }

    private void Flip(Collider2D target)
    {
        facingRight = (target.transform.position.x - transform.position.x) > 0;
        Debug.Log("Flip");
        transform.Rotate(Vector3.up, 180);
    }

    private void Shoot(Collider2D target)
    {
        GameObject go = Instantiate(projectilePrefab, firingPosition.position, Quaternion.identity);
        Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
        if (!facingRight)
        {
            go.transform.Rotate(Vector3.up, 180);
        }
        Vector3 targetPos = target.transform.position - transform.position;
        targetPos = targetPos.normalized;
        rb2d.AddForce((targetPos * shootForce));
    }

    private void JumpAction()
    {
        Debug.Log("JumpAction");
        if (Physics2D.Raycast(transform.position, transform.right, 2f, groundLayer))
        {
            facingRight = !facingRight;
            transform.Rotate(Vector3.up, 180);
        }
        _rb2d.AddForce((transform.right + transform.up * 5).normalized * jumpForce);
    }

    private void OnDrawGizmos()
    {
        if (!drawAttackAreaGizmo) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col2d.bounds.center + (Vector3)offset, attackArea);
        
        Gizmos.DrawLine(transform.position, transform.position+(transform.right*2f));
    }
}
