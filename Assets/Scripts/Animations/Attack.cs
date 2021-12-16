using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    private IDamager _damager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _damager = animator.GetComponentInParent<IDamager>();
        _damager.IsAttacking = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.normalizedTime > 0.976f)
        {
            _damager.IsAttacking = false;
            _damager.DisableHitbox();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _damager.IsAttacking = false;
        _damager.DisableHitbox();
    }
}
