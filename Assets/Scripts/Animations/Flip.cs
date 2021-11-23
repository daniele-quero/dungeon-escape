using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("player_swing_attack"))
            animator.GetComponent<SpriteRenderer>().flipX = animator.GetComponent<SpriteRenderer>().flipX;
        else
        animator.GetComponent<SpriteRenderer>().flipX = (animator.GetComponentInParent<PlayerMovement>().Velocity.x < 0);
    }
}
