using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int _diamonds;
    [SerializeField] private List<Item> _inventory;

    public int Diamonds { get => _diamonds; set => _diamonds = value; }
    public List<Item> Inventory { get => _inventory; }

    public void AddDiamonds(int val)
    {
        _diamonds += val;
    }


}
