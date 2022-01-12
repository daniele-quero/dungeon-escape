using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Diamond : MonoBehaviour
{
    private int value;
    [SerializeField] private AudioSource _sound;
    public int Value { get => value; set => this.value = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerInventory invt = null;
            if (collision.TryGetComponent<PlayerInventory>(out invt))
            {
                invt.AddDiamonds(value);
                if (_sound != null)
                    _sound.Play();
            }
        }
    }
}
