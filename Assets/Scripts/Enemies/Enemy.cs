using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAggro))]
[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyCombat))]
public abstract class Enemy : MonoBehaviour
{
    #region Variables: Stats
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _gems;
    #endregion

    #region Sub-Behaviours References
    protected EnemyPatrol _patrol;
    protected EnemyAggro _aggro;
    protected EnemyCombat _combat;
    #endregion

    #region Variables: Model
    protected SpriteRenderer _model;
    protected Animator _animator;
    private WaitForSeconds _walkCheckWait;
    #endregion

    #region Properties
    public float Speed { get => _speed; set => _speed = value; }
    public float OriginalSpeed { get; set; }
    public int Health { get => _health; set => _health = value; }
    public SpriteRenderer Model { get => _model; }
    public Animator Animator { get => _animator; }
    public EnemyPatrol Patrol { get => _patrol; }
    public EnemyAggro Aggro { get => _aggro; }
    public EnemyCombat Combat { get => _combat; }
    #endregion

    #region MonoBehaviour Methods
    protected abstract void Update();

    protected virtual void Start()
    {
        _model = Utils.SafeGetComponentInChildren<SpriteRenderer>(this);
        _animator = Utils.SafeGetComponentInChildren<Animator>(this);
        _patrol = GetComponent<EnemyPatrol>();
        _aggro = GetComponent<EnemyAggro>();
        _combat = GetComponent<EnemyCombat>();
        OriginalSpeed = Speed;
    }
    #endregion

    #region Idle Management
    public void ToggleIdle()
    {
        _animator.SetBool("isIdle", !(_patrol.isPatrolling || _aggro.isAggro));
    }

    public void ToggleIdle(bool isIdle)
    {
        _animator.SetBool("isIdle", isIdle);
    }
    #endregion
}