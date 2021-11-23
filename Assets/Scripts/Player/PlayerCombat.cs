using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerAnimations _pa;
    private PlayerMovement _pm;
    private bool _isAttacking = false;

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

    void Start()
    {
        _pa = GetComponent<PlayerAnimations>();
        _pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _pm.IsGrounded() && !_isAttacking)
        {
            _pa.SetAttackTrigger();
        }
    }
}
