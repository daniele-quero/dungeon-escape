using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{

    #region Combat Stats
    [SerializeField] protected int _health;
    [SerializeField] protected int _MaxHealth;
    [SerializeField] protected int _poise;
    [SerializeField] protected List<Damage> _defencesList;
    protected Dictionary<DmgType, int> _defences = new Dictionary<DmgType, int>();
    public WaitForSeconds Stagger { get; set; }
    public int Poise { get => _poise; set => UpdatePoise(value); }
    #endregion

    protected virtual void Start()
    {
        foreach (var d in _defencesList)
            _defences.Add(d.type, d.amount);

        UpdatePoise(_poise);
    }

    private void UpdatePoise(int p)
    {
        _poise = p;
        Stagger = new WaitForSeconds(CombatMathf.StaggerTime(_poise));
    }
    
}
