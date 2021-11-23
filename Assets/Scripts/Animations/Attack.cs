using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    private PlayerCombat _pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _pc = animator.GetComponentInParent<PlayerCombat>();
        _pc.IsAttacking = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime > 0.976f)
            _pc.IsAttacking = false;
    }
}
