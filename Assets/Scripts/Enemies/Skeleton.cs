using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Start()
    {
        base.Start();
        _combat.HitCooldown = new WaitForSeconds(0.25f);
    }

    protected override void Update()
    {
        _aggro.Aggro(); 
        _patrol.Patrol();
    }
}
