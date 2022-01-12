using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Damage 
{
    public int amount;
    public DmgType type; 
}

public enum DmgType
{
    normal, 
    fire,
    magic,
    poison,

}