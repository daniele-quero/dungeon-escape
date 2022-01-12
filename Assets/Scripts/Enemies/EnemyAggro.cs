using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAggro : Walker, IWalker
{
    private Enemy _enemy;

    #region Varaibles: Aggro
    private Transform _player;
    private AggroRange _aggro;
    private Vector3 _reverse;
    public bool isAggro;
    #endregion

    public AggroRange Range { get => _aggro; }

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _aggro = Utils.SafeGetComponentInChildren<AggroRange>(this);
        _reverse = new Vector3(-1, 1, 1);
    }

    #region Aggro Routine
    public void Aggro()
    {
        if (PlayerInSight())
        {
            if (XDistance(_aggro.Player) > 1f) //TBD value
            {
                isAggro = true;
                _enemy.Patrol.isPatrolling = false;
                _enemy.Combat.IsAttacking = false;
                MoveToPlayer();
                _enemy.ToggleIdle(false);
            }
            else
            {
                FlipUnflip();
                _enemy.Combat.Attack();
                isAggro = false;
                _enemy.Patrol.isPatrolling = false;
                _enemy.ToggleIdle(true);
            }
        }
        else
        {
            isAggro = false;
            _enemy.Patrol.isPatrolling = true;
            _enemy.Combat.IsAttacking = false;
        }
    }

    private void MoveToPlayer()
    {
        if (isAggro)
        {
            FlipUnflip();

            Vector3 target = _aggro.Player.position;
            target.y = transform.position.y;


            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _enemy.Speed);
        }
    }

    public void FlipRange(bool leftToRight)
    {
        _aggro.transform.localScale = leftToRight ? _reverse : Vector3.one;
    }
    #endregion

    public IEnumerator AggroFade(WaitForSeconds fade)
    {
        yield return fade;
        _aggro.Player = null;
    }

    #region IWalker implementation
    public void FlipUnflip()
    {
        if (_enemy.Speed > 0)
        {
            IsFlipped = LeftToRight();

            if (IsFlipped)
                Flip(_enemy.Model.transform);
            else
                UnFlip(_enemy.Model.transform);

            FlipRange(IsFlipped);
        }
    }
    #endregion

    #region Checks
    private float XDistance(Transform target)
    {
        return Mathf.Abs(transform.position.x - target.position.x);
    }

    public bool PlayerInSight()
    {
        return _aggro.Player != null;
    }

    private bool LeftToRight()
    {
        return transform.position.x > _aggro.Player.position.x;
    }
    #endregion
}
