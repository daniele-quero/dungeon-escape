using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        Patrol();
    }

 }
