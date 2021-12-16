using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        _patrol.Patrol();
    }
}
