using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatMathf 
{
    public const int maxPoise = 20; 

    public static float StaggerTime(int poise)
    {
        return (float)(maxPoise - poise) / (float)maxPoise;
    }

    public static int NetDamage(Damage dmg, Dictionary<DmgType, int> _defences)
    {
        _defences.TryGetValue(dmg.type, out int def);

        int net = dmg.amount - def;
        return net > 0 ? net : 0;
    }
}
