using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager
{
    bool IsAttacking { get; set; }
    void DisableHitbox();
}
