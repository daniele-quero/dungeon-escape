using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Diamond : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private AudioSource _sound;
    public int Value { get => _value; set => this._value = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerInventory invt = null;
            if (collision.TryGetComponent<PlayerInventory>(out invt))
            {
                invt.AddDiamonds(_value);
                if (_sound != null)
                    _sound.Play();

                Destroy(this.gameObject);
            }
        }
    }
}
