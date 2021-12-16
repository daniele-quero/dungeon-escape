using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Walker : MonoBehaviour
{
    public bool IsFlipped { get; set; }
    protected void Flip(Transform t)
    {
        t.localRotation = Quaternion.Euler(0, 180f, 0);
    }

    protected void UnFlip(Transform t)
    {
        t.localRotation = Quaternion.identity;
    }
}
