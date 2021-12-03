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
        if (animatorStateInfo.normalizedTime > 0.976f)
        {
            _pc.IsAttacking = false;
            DisableSwordHitBox(animator);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DisableSwordHitBox(animator);
    }

    private void DisableSwordHitBox(Animator animator)
    {
        var swordHitBox = animator.GetComponentInChildren<BoxCollider2D>();
        swordHitBox.transform.localPosition = Vector3.zero;
        swordHitBox.transform.localRotation = Quaternion.identity;
        swordHitBox.offset = new Vector2(-0.00161226094f, 0.0288518965f);
        swordHitBox.size = new Vector2(0.380404323f, 0.0761577785f);
        animator.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    private void LootTable()
    {
        float RNG = Random.Range(0f, 1f);

        if (RNG > 0.5) //%50 percent chance
        {
            Debug.Log("Prize 1");
        }

        if (RNG > 0.2) //%80 percent chance (1 - 0.2 is 0.8)
        {
            Debug.Log("Prize 2");
        }

        if (RNG > 0.7) //%30 percent chance (1 - 0.7 is 0.3)
        {
            Debug.Log("Prize 3");
        }
    }

}
