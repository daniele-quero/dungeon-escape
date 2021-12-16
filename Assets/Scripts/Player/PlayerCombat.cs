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

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _pm.IsGrounded() && !_isAttacking)
        {
            //_swordHitBox.enabled = true;
            _pa.SetAttackTrigger();
        }
    }

    #region IDamager implementation
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

    public void DisableHitbox()
    {
        _swordHitBox.transform.localRotation = Quaternion.identity;
        _swordHitBox.enabled = false;
    }
    #endregion
}
