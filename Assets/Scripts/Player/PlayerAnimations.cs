using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private Animator _swordAnimator;

    void Start()
    {
        _animator = GetComponentsInChildren<Animator>()[0];
        _swordAnimator = GetComponentsInChildren<Animator>()[1];
    }

    public void SetSpeedParameter(float s)
    {
        _animator.SetFloat("speed", s);
    }

    public void SetJumpTrigger()
    {
        _animator.SetTrigger("onJump");
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger("onAttack");
        _swordAnimator.SetTrigger("onAttack");
    }
}
