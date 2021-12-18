using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCombat : MonoBehaviour, IDamager
{
    private PlayerAnimations _pa;
    private PlayerMovement _pm;
    [SerializeField]private bool _isAttacking = false;
    [SerializeField] private BoxCollider2D _swordHitBox;

    void Start()
    {
        _pa = GetComponent<PlayerAnimations>();
        _pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Attack();
    }

    #region IDamager implementation    
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _pm.IsGrounded() && !_isAttacking)
            _pa.SetAttackTrigger();
    }

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

    public void DisableHitbox()
    {
        _swordHitBox.enabled = false;
    }
    #endregion
}
