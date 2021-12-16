using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MossGiant : Enemy
{
    protected override void Start()
    {
        base.Start();
        _combat.Stagger = new WaitForSeconds(1f);
        _combat.HitCooldown = new WaitForSeconds(0.25f);
    }

    protected override void Update()
    {
        _patrol.Patrol();
        _aggro.Aggro();
    }
}
