using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    [SerializeField] private PlayerMovement _pm;
    [SerializeField] private PlayerAnimations _pa;

    public void InAir()
    {
        _pm.CanMove = false;
        _pa.ResetLandingTrigger();
    }

    public void Land()
    {
        _pa.ResumeFromHitTrigger();
        _pm.CanMove = true;
    }
}
