using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private Animator _swordAnimator;
    [SerializeField] private AudioSource _hitAudio;
    [SerializeField] private AudioSource _attack1Audio;
    [SerializeField] private AudioSource _jumpAudio;
    [SerializeField] private AudioSource _landAudio;

    void Start()
    {
        _animator = GetComponentsInChildren<Animator>()[0];
        _swordAnimator = GetComponentsInChildren<Animator>()[1];
    }

    public void SetSpeedParameter(float s)
    {
        _animator.SetFloat("speed", s);
    }

    public void SetLandingTrigger()
    {
        _landAudio.Play();
        _animator.SetTrigger("onLanding");
    }

    public void SetJumpTrigger()
    {
        _jumpAudio.Play();
        _animator.SetTrigger("onJump");
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger("onAttack");
        _swordAnimator.SetTrigger("onAttack");
    }

    public void SetHitTrigger()
    {
        _hitAudio.Play();
        _animator.SetTrigger("onHit");
    }

    public void ResumeFromHitTrigger()
    {
        _animator.SetTrigger("onResume");
    }

    public void SetDeathTrigger()
    {
        _animator.SetTrigger("onDeath");
    }
}
