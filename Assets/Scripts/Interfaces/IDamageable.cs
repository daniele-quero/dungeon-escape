using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Health { get; set; }
    bool IsHit { get; set; }
    void Damage(List<Damage> dmg, Transform source);
    void Kill();
    void Death();
    IEnumerator TakeHit(Transform source);
}
