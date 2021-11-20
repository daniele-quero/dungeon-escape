using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeedParameter(float s)
    {
        _animator.SetFloat("speed", s);
    }

    public void SetJumpTrigger()
    {
        _animator.SetTrigger("onJump");
    }
}
