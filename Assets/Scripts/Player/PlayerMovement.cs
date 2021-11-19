using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private BoxCollider2D _bc2d;
    [SerializeField] private float _walkSpeed, _runSpeed, _jumpHeight;
    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [SerializeField] private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
        _velocity.x = Input.GetAxis("Horizontal") * speed;
        _velocity.y = Jump();
        _rb2d.velocity = _velocity;
    }

    private float Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            return Mathf.Sqrt(-2 * _jumpHeight * Physics2D.gravity.y);

        return _rb2d.velocity.y;
    }

    private bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position;

        if (_bc2d != null)
        {
            origin += _bc2d.offset;
            origin.y -= _bc2d.size.y * 0.505f;
        }

        Vector2 dir = Vector2.down * 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down,  0.1f);
        if (hit.collider != null)
        {
            Debug.Log("player ray hitting: " + hit.collider.name);
            Debug.DrawRay(origin, Vector2.down * 0.1f);
            return hit.collider.name.ToLower().Contains("ground");
        }

        return false;
    }
}
