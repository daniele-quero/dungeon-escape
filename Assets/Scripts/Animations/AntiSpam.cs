using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSpam : MonoBehaviour
{
    public void NoSpam()
    {
        IDamager dmg;
        if (transform.parent != null && transform.parent.TryGetComponent<IDamager>(out dmg))
            dmg.IsAttacking = false;
    }

    public void StartAttack()
    {
        IDamager dmg;
        if (transform.parent != null && transform.parent.TryGetComponent<IDamager>(out dmg))
            dmg.IsAttacking = true;
    }
}
