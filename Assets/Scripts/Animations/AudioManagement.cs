using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<EnemyAudio>().StopAttackAudio();
    }
}
