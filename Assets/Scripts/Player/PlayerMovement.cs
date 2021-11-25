using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private BoxCollider2D _bc2d;
    [SerializeField] private float _walkSpeed, _runSpeed, _jumpHeight;
    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [SerializeField] private bool _isGrounded;
    [SerializeField] float _jumpAxis;
    private int _groundLayer;
    private PlayerAnimations _pa;
    private Vector2 _boxCastSize;
    private Vector2 _colliderOffset;
    private float _boxCastDistance = 0.6f;
    private SpriteRenderer _model;
    private float _jumpCoolDown = 1f;

    public Vector2 Velocity { get => _velocity; }

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bc2d = GetComponent<BoxCollider2D>();
        _pa = GetComponent<PlayerAnimations>();
        _groundLayer = LayerMask.GetMask("Ground");
        _boxCastSize = new Vector2(_bc2d.size.x * 1.2f, _bc2d.size.y);
        _model = GetComponentInChildren<SpriteRenderer>();
        _colliderOffset = _bc2d.offset;
    }

    // Update is called once per frame
    void Update()
    {
        SetColliderOffset();
        Move();
    }

    private void Move()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) && IsGrounded() ? _runSpeed : _walkSpeed;
        _velocity.x =  Input.GetAxis("Horizontal") * speed;
        if(IsGrounded())
            _pa.SetSpeedParameter(Mathf.Abs(_velocity.x));
        _velocity.y = Jump();
        _rb2d.velocity = _velocity;
    }

    private float Jump()
    {
        _jumpAxis = Input.GetAxis("Jump");

        if (_jumpAxis>0 && IsGrounded())
        {
            _pa.SetJumpTrigger();
            return Mathf.Sqrt(-2 * _jumpHeight * Physics2D.gravity.y * _jumpAxis);
        }

        return _rb2d.velocity.y;
    }

    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(GetOrigin(), _boxCastSize, 0, Vector2Int.down, _boxCastDistance, _groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private void SetColliderOffset()
    {
        _colliderOffset.x = _model.flipX ? 0.08f : -0.08f;
        _bc2d.offset = _colliderOffset;
    }

    private Vector2 GetOrigin()
    {
        return _model.flipX
            ? (Vector2)transform.position + Vector2.right * 0.1f
            : (Vector2)transform.position + Vector2.left * 0.1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(GetOrigin() + Vector2.down * _boxCastDistance, _boxCastSize);
    }
}
