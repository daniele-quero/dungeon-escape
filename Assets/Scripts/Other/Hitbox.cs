using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    [SerializeField] private List<Damage> dmg;
    [SerializeField] private Transform _source;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(dmg, _source);

            if (damageable.Health < 1)
                damageable.Kill();
        }
    }
}
