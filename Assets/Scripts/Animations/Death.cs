using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void OnDeath()
    {
        GetComponentInParent<IDamageable>().Death();
    }

    public void BeforeDeath()
    {
        GetComponentInParent<Collider2D>().enabled = false;
    }
}
