using UnityEngine;
using Pathfinding;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Simple patrol behavior.
/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="_targets"/> array.
/// Upon reaching a target it will wait for <see cref="delay"/> seconds.
/// </summary>
public class CustomPatrol : MonoBehaviour {
    /// <summary>Target points to move to in order</summary>
    public SOPatrol PatrolWaypoints;

    /// <summary>Time in seconds to wait at each target</summary>

    /// <summary>Current target index</summary>
    int index;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    private void Awake () {
        agent = GetComponent<IAstarAI>();
    }

    private void Start()
    {
    }

    /// <summary>Update is called once per frame</summary>
    private void Update () {
        if (PatrolWaypoints.Waypoints.Length == 0) return;

        bool search = false;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime)) {
            switchTime = Time.time + PatrolWaypoints.Delay;
        }

        if (Time.time >= switchTime) {
            index = index + 1;
            search = true;
            switchTime = float.PositiveInfinity;
        }

        index = index % PatrolWaypoints.Waypoints.Length;
        agent.destination = PatrolWaypoints.Waypoints[index];

        if (search) agent.SearchPath();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (enabled == false || PatrolWaypoints == null || PatrolWaypoints.Waypoints.Length == 0)
            return;

        Gizmos.color = Color.cyan; 
        Handles.color = Color.white;
        for (int i = 0; i < PatrolWaypoints.Waypoints.Length; i++)
        {
            Handles.DrawWireDisc(PatrolWaypoints.Waypoints[i], Vector3.back, 0.1f);
        }

        // draw lines between PatrolWaypoints.Waypoints
        for (int i = 0; i < PatrolWaypoints.Waypoints.Length; i++)
        {
            if (i == PatrolWaypoints.Waypoints.Length - 1)
                Gizmos.DrawLine(PatrolWaypoints.Waypoints[i], PatrolWaypoints.Waypoints[0]);
            else
                Gizmos.DrawLine(PatrolWaypoints.Waypoints[i], PatrolWaypoints.Waypoints[i + 1]);
        }
    }
#endif
}
