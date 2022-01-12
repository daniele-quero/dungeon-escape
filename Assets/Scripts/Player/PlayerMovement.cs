using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Walker, IWalker
{
    #region Variables
    private Rigidbody2D _rb2d;
    private BoxCollider2D _bc2d;
    private SpriteRenderer _model;
    private PlayerAnimations _pa;
    private PlayerCombat _pc;
    private Vector2 _boxCastSize;
    private Vector2 _colliderOffset;
    private float _boxCastDistance = 0.6f;
    private int _groundLayer;
    #endregion

    #region Serialized Fields
    [SerializeField] private float _walkSpeed, _runSpeed, _jumpHeight;
    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [SerializeField] private bool _isGrounded;
    [SerializeField] float _jumpAxis;
    #endregion

    public Vector2 Velocity { get => _velocity; set => _velocity = value; }

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bc2d = GetComponent<BoxCollider2D>();
        _pa = GetComponent<PlayerAnimations>();
        _pc = GetComponent<PlayerCombat>();
        _groundLayer = LayerMask.GetMask("Ground");
        _boxCastSize = new Vector2(_bc2d.size.x * 1.2f, _bc2d.size.y);
        _model = GetComponentInChildren<SpriteRenderer>();
        _colliderOffset = _bc2d.offset;
    }

    void Update()
    {
        if (!_pc.IsHit)
            Move();
    }

    #region Movement
    private void Move()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) && IsGrounded() ? _runSpeed : _walkSpeed;
        _velocity.x = Input.GetAxis("Horizontal") * speed;

        if (IsGrounded())
            _pa.SetSpeedParameter(Mathf.Abs(_velocity.x));

        _velocity.y = Jump();
        //AdjustVelocity();
        _rb2d.velocity = _velocity;

        FlipUnflip();
        if (IsGrounded() && _velocity.y < -0.5f)
            _pa.SetLandingTrigger();
    }

    private void AdjustVelocity()
    {
        if (IsGrounded() && _rb2d.velocity.x != 0)
            _velocity.y = 0;
    }

    private float Jump()
    {
        _jumpAxis = Input.GetAxis("Jump");

        if (_jumpAxis > 0 && IsGrounded())
        {
            _pa.SetJumpTrigger();
            return Mathf.Sqrt(-2 * _jumpHeight * Physics2D.gravity.y * _jumpAxis);
        }

        if (IsGrounded() && _rb2d.velocity.x != 0)
            return 0;
        return _rb2d.velocity.y;
    }

    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(GetOrigin(), _boxCastSize, 0, Vector2Int.down, _boxCastDistance, _groundLayer);
        if (hit.collider != null)
            return true;

        return false;
    }
    #endregion

    private void SetColliderOffset()
    {
        _colliderOffset.x = IsFlipped ? 0.08f : -0.08f;
        _bc2d.offset = _colliderOffset;
    }

    #region Gizmo
    private Vector2 GetOrigin()
    {
        return IsFlipped
            ? (Vector2)transform.position + Vector2.right * 0.1f
            : (Vector2)transform.position + Vector2.left * 0.1f;
    }

    private void OnDrawGizmos()
    {
        if (_model != null)
            Gizmos.DrawWireCube(GetOrigin() + Vector2.down * _boxCastDistance, _boxCastSize);
    }
    #endregion

    #region IWalker implementation
    public void FlipUnflip()
    {
        if (_velocity.x < 0)
        {
            IsFlipped = true;
            Flip(_model.transform);
            SetColliderOffset();
        }
        else if (_velocity.x > 0)
        {
            IsFlipped = false;
            UnFlip(_model.transform);
            SetColliderOffset();
        }

    }
    #endregion
}
