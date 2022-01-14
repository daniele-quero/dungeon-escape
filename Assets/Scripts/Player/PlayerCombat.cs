using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCombat : Combat, IDamager, IDamageable
{
    private PlayerAnimations _pa;
    private PlayerMovement _pm;
    
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private BoxCollider2D _swordHitBox;
    
    protected override void Start()
    {
        base.Start();
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
        {
            _pa.SetAttackTrigger();
            IsAttacking = true;
        }
    }

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    #endregion

    #region IDamageable implementation
    public bool IsHit { get; set; }

    public int Health { get => _health; set => _health = value; }

    public void DisableHitbox() => _swordHitBox.enabled = false;

    public void Damage(List<Damage> dmg, Transform source)
    {
        if (!IsHit)
        {
            foreach (var d in dmg)
                Health -= CombatMathf.NetDamage(d, _defences);

            StartCoroutine(TakeHit(source));
        }
    }

    public void Kill() => _pa.SetDeathTrigger();

    public void Death()
    {
        throw new System.NotImplementedException();
        //TODO: code game over
    }

    public IEnumerator TakeHit(Transform source)
    {
        _pa.SetHitTrigger();
        IsHit = true;
        _pm.CanMove = false;
        yield return Stagger;
        _pa.ResumeFromHitTrigger();
        IsAttacking = false;
        IsHit = false;
        _pm.CanMove = true;
    }
    #endregion
}
