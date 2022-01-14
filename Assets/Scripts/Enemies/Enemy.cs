using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAggro))]
[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(EnemyAudio))]
public abstract class Enemy : MonoBehaviour
{
    #region Variables: Stats
    //[SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _diamonds;
    #endregion

    #region Sub-Behaviours References
    protected EnemyPatrol _patrol;
    protected EnemyAggro _aggro;
    protected EnemyCombat _combat;
    protected EnemyAudio _audio;
    #endregion

    #region Variables: Model
    protected SpriteRenderer _model;
    protected Animator _animator;
    private WaitForSeconds _walkCheckWait;
    #endregion

    #region Properties
    public float Speed { get => _speed; set => _speed = value; }
    public float OriginalSpeed { get; set; }
    public SpriteRenderer Model { get => _model; }
    public Animator Animator { get => _animator; }
    public EnemyPatrol Patrol { get => _patrol; }
    public EnemyAggro Aggro { get => _aggro; }
    public EnemyCombat Combat { get => _combat; }
    public EnemyAudio Audio { get => _audio; }
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
        _audio = GetComponent<EnemyAudio>();
        OriginalSpeed = Speed;
    }
    #endregion

    #region Idle Management
    public void ToggleIdle()
    {
        bool isIdle = !(Patrol.isPatrolling || Aggro.isAggro );
        Audio.ToggleMuteIdle(isIdle);
        Animator.SetBool("isIdle", isIdle);
    }

    public void ToggleIdle(bool isIdle)
    {
        Audio.ToggleMuteIdle(isIdle);
        Animator.SetBool("isIdle", isIdle);
    }


    #endregion

    public void DropDiamonds()
    {
        //todo: drop diamond or diamonds
    }
}