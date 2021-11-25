using UnityEngine;

public class Flip : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spriteRenderer = animator.GetComponent<SpriteRenderer>();

        if (stateInfo.IsName("player_swing_attack"))
            spriteRenderer.flipX = spriteRenderer.flipX;
        else
        spriteRenderer.flipX = (animator.GetComponentInParent<PlayerMovement>().Velocity.x < 0);


    }
}
