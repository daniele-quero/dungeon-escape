using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    #region Variables: Stats
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _gems;
    #endregion

    #region Variables: Patrol 
    [SerializeField] protected List<Transform> _waypoints;
    private int _currentTargetId = 0;
    private int _direction = -1;
    public bool waitOnWaypoint = false;
    public bool isPatrolling = true;
    public float idleOnLimitTime = 1.5f;
    public float idleOnWaypointTime = 0f;
    private WaitForSeconds _idleOnLimit;
    private WaitForSeconds _idleOnWaypoint;
    #endregion

    #region Variables: Model
    protected SpriteRenderer _model;
    protected Animator _animator;
    #endregion

    #region Properties
    public float Speed { get => _speed; set => _speed = value; }
    public int Gems { get => _gems; set => _gems = value; }
    public int Health { get => _health; set => _health = value; }
    #endregion

    #region MonoBehaviour Methods
    protected abstract void Update();

    protected virtual void Start()
    {
        _idleOnLimit = new WaitForSeconds(idleOnLimitTime);
        _idleOnWaypoint = waitOnWaypoint ? new WaitForSeconds(idleOnWaypointTime) : null;
        _model = GetComponentInChildren<SpriteRenderer>();
        if (_model == null)
            Debug.LogError("No Sprite found on " + name);

        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.LogError("No Animator found on " + name);
    }
    #endregion

    #region Patrol Routine
    protected void Patrol()
    {
        if (isPatrolling && WaypointsGood())
        {
            if (transform.position == _waypoints[_currentTargetId].position)
            {
                if (OnWaypointsLimits())
                    StartCoroutine(PausePatrol(_idleOnLimit));
                else if (waitOnWaypoint)
                    StartCoroutine(PausePatrol(_idleOnWaypoint));

                _currentTargetId = NextTarget();
            }

            MoveTo(_currentTargetId);
        }
    }

    private IEnumerator PausePatrol(WaitForSeconds wait)
    {
        float oldSpeed = _speed;
        _speed = 0;
        _animator.SetBool("isIdle", true);

        yield return wait;

        _speed = oldSpeed;
        _animator.SetBool("isIdle", false);
    }

    private void MoveTo(int id)
    {
        if (_speed > 0)
            _model.flipX = LeftToRight();

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[id].position, Time.deltaTime * _speed);
    }

    private int NextTarget()
    {
        if (OnWaypointsLimits())
            if (_currentTargetId == 0)
                _direction = 1;
            else
                _direction = -1;

        int next = _currentTargetId + _direction;
     
        return IdGood(next) ? next : next - _direction;
    }
    #endregion

    #region Waypoint Checks
    private bool LeftToRight()
    {
        return transform.position.x > _waypoints[_currentTargetId].position.x;
    }

    private bool WaypointsGood()
    {
        return _waypoints != null && _waypoints.Count > 0 && !_waypoints.Contains(null);
    }

    private bool OnWaypointsLimits()
    {
        return transform.position == _waypoints[0].position
            || transform.position == _waypoints[_waypoints.Count - 1].position;
    }

    private bool IdGood(int id)
    {
        return id >= 0 && id < _waypoints.Count;
    }
    #endregion
}