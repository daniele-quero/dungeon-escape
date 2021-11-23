using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAttack : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.GetComponent<SpriteRenderer>().flipX = 
            animator.GetComponent<SpriteRenderer>().flipY = 
            animator.transform.parent.GetComponentsInChildren<SpriteRenderer>()[0].flipX;
    }


}
