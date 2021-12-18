using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyCombat : MonoBehaviour, IDamageable, IDamager
{
    private Enemy _enemy;
    [SerializeField] private bool _inCombat;
    [SerializeField] private bool _isAttacking = false;

    #region Properties
    public WaitForSeconds Stagger { get; set; }
    public WaitForSeconds HitCooldown { get; set; }
    #endregion

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        IsHit = false;
        IsAttacking = false;
    }

    #region IDamageable Implementation
    public int Health { get => _enemy.Health; set => _enemy.Health = value; }
    public bool IsHit { get; set; }

    public IEnumerator TakeHit(Transform source)
    {
        _enemy.Speed = 0;
        _enemy.Patrol.isPatrolling = false;

        _enemy.Animator.SetTrigger("onHit");
        IsHit = true;

        yield return HitCooldown;
        IsHit = false;

        if (Random.Range(0f, 1f) < 0.7f)
            yield return Stagger;

        _enemy.Animator.SetTrigger("onResume");
        _enemy.Speed = _enemy.OriginalSpeed;
        _enemy.Aggro.Range.Player = source;

        StartCoroutine(_enemy.Aggro.AggroFade(new WaitForSeconds(2.5f)));
    }

    public void Damage(List<Damage> dmg, Transform source)
    {
        if (!IsHit)
        {
            foreach (var d in dmg)
            {
                Health -= d.amount;
            }

            StartCoroutine(TakeHit(source));
        }
    }

    public void Kill()
    {
        _enemy.Animator.SetTrigger("onDeath");
    }

    public void Death()
    {
        var components = GetComponentsInChildren<Behaviour>();
        foreach (var c in components)
            if (c != this)
                c.enabled = false;

        GameObject.Destroy(gameObject, 30f);
    }
    #endregion

    #region IDamager Implementation
    public void Attack()
    {
        if (!_isAttacking)
        {
            _enemy.Animator.SetTrigger("onAttack");
        }
    }

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public void DisableHitbox()
    {
        _enemy.Model.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
    #endregion
}
