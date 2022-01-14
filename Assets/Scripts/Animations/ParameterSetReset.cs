using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterSetReset : MonoBehaviour
{
    [SerializeField] Animator _animator;
    public void ResetTrigger(string s)
    {
        _animator.ResetTrigger(s);
    }

    public void SetTrigger(string s)
    {
        _animator.SetTrigger(s);
    }
}
