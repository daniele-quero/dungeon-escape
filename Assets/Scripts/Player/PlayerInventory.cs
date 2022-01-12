using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int _diamonds;

    public int Diamonds { get => _diamonds; set => _diamonds = value; }

    public void AddDiamonds(int val)
    {
        _diamonds += val;
    }
}
