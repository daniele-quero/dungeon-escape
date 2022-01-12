using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public bool shotDone = true;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        _patrol.Patrol();
        SpiderAttack();
    }

    private void SpiderAttack()
    {
        if (_aggro.PlayerInSight())
        {
            _aggro.FlipUnflip();
            _patrol.isPatrolling = false;
            if (!_combat.IsAttacking)
                _animator.SetTrigger("onAttack");
        }
        else
        {
            _patrol.isPatrolling = shotDone;//todo: shotDone backup
            _combat.IsAttacking = false;
        }
    }
}
