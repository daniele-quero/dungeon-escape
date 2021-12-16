using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
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
