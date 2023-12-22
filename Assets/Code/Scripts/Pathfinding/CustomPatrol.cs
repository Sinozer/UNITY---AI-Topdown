using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Simple patrol behavior.
/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="_targets"/> array.
/// Upon reaching a target it will wait for <see cref="delay"/> seconds.
/// </summary>
public class CustomPatrol : MonoBehaviour
{
    /// <summary>Target points to move to in order</summary>
    public List<Vector3> Waypoints
    {
        get => _waypoints;
        set => _waypoints = value;
    }
    [SerializeField] private List<Vector3> _waypoints = new List<Vector3>();

    /// <summary>Time in seconds to wait at each target</summary>

    /// <summary>Current target index</summary>
    private int _index;

    private IAstarAI _agent;
    private float _switchTime = float.PositiveInfinity;

    private void Awake()
    {
        _agent = GetComponent<IAstarAI>();
    }

    private void Start()
    {
    }

    /// <summary>Update is called once per frame</summary>
    private void Update()
    {
        if (_waypoints.Count == 0) return;

        bool search = false;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (_agent.reachedEndOfPath && !_agent.pathPending && float.IsPositiveInfinity(_switchTime))
        {
            var _delay = Random.Range(0f, 3f);
            _switchTime = Time.time + _delay;
        }

        if (Time.time >= _switchTime)
        {
            _index++;
            search = true;
            _switchTime = float.PositiveInfinity;
        }

        _index %= _waypoints.Count;
        _agent.destination = _waypoints[_index];

        if (search) _agent.SearchPath();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (enabled == false || _waypoints == null || _waypoints.Count == 0)
            return;

        Gizmos.color = Color.cyan;
        Handles.color = Color.white;
        for (int i = 0; i < _waypoints.Count; i++)
        {
            Handles.DrawWireDisc(_waypoints[i], Vector3.back, 0.1f);
        }

        // draw lines between PatrolWaypoints.Waypoints
        for (int i = 0; i < _waypoints.Count; i++)
        {
            if (i == _waypoints.Count - 1)
                Gizmos.DrawLine(_waypoints[i], _waypoints[0]);
            else
                Gizmos.DrawLine(_waypoints[i], _waypoints[i + 1]);
        }
    }
#endif
}
