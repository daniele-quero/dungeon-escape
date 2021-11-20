using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SpriteRenderer>().flipX = (animator.GetComponentInParent<PlayerMovement>().Velocity.x < 0);
    }

}
