using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _attackAudio;
    [SerializeField] private AudioSource _idleAudio;
    [SerializeField] private AudioSource _hitAudio;
    [SerializeField] private AudioSource _deathAudio;

    public void ToggleMuteIdle(bool isIdle)
    {
        if (_idleAudio != null)
            _idleAudio.mute = !isIdle;
    }

    public void PlayDeathAudio()
    {
        if (_deathAudio != null)
            _deathAudio.Play();
    }

    public void PlayHitAudio()
    {
        if (_hitAudio != null)
            _hitAudio.Play();
    }

    public void PlayAttackAudio()
    {
        if (_attackAudio != null)
            _attackAudio.Play();
    }

    public void StopAttackAudio()
    {
        if (_attackAudio != null)
            _attackAudio.Stop();
    }

    public void StopHitAudio()
    {
        if (_hitAudio != null)
            _hitAudio.Stop();
    }

}
