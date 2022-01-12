using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyPatrol : Walker, IWalker
{
    private Enemy _enemy;

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

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _idleOnLimit = new WaitForSeconds(idleOnLimitTime);
        _idleOnWaypoint = waitOnWaypoint ? new WaitForSeconds(idleOnWaypointTime) : null;
        isPatrolling = _waypoints.Count > 1;
        _enemy.ToggleIdle();
    }

    #region Patrol Routine
    public void Patrol()
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
        float oldSpeed = _enemy.Speed;
        _enemy.Speed = 0;
        _enemy.ToggleIdle(true);

        yield return wait;

        _enemy.ToggleIdle(false);
        _enemy.Speed = oldSpeed;
    }

    private void MoveTo(int id)
    {
        FlipUnflip();
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[id].position, Time.deltaTime * _enemy.Speed);
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

    #region IWalker implementation
    public void FlipUnflip()
    {
        if (_enemy.Speed > 0)
        {
            IsFlipped = LeftToRight();

            if (IsFlipped)
                Flip(_enemy.Model.transform);
            else
                UnFlip(_enemy.Model.transform);

            _enemy.Aggro.FlipRange(IsFlipped);
        }
    }
    #endregion
}
