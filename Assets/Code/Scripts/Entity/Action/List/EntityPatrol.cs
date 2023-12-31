#if UNITY_EDITOR
using UnityEditor;
#endif

using Sirenix.OdinInspector;
using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Simple patrol behavior.
/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="Waypoints"/> array.
/// Upon reaching a target it will wait for a random delay between <see cref="DelayRange"/> seconds.
/// </summary>
public class EntityPatrol : EntityChild, IEntityAction
{
    /// <summary>
    /// Reference to the <see cref="IAstarAI"/> component on the entity
    /// </summary>
    private IAstarAI Agent
    {
        get
        {
            if (_agent == null)
                _agent = Entity.GetComponentInChildren<IAstarAI>(true);

            return _agent;
        }
    }
    private IAstarAI _agent;

    /// <summary>
    /// List of waypoints to move to in order
    /// </summary>
    [ShowInInspector]
    public List<Vector3> Waypoints { get; set; } = new List<Vector3>();

    /// <summary>
    /// Delay time in seconds between reaching a waypoint and start moving to the next one
    /// </summary>
    [MinMaxSlider(0, 10)] public Vector2 DelayRange { get; set; } = new Vector2(0, 3);

    /// <summary>
    /// Current waypoint index in the <see cref="Waypoints"/> array
    /// </summary>
    public int Index { get; set; }

    private Coroutine _patrolCoroutine;

    private void OnEnable()
    {
        _patrolCoroutine = StartCoroutine(StartPatrol());
    }
    private void OnDisable()
    {
        if (_patrolCoroutine == null)
            return;

        StopCoroutine(_patrolCoroutine);
        _patrolCoroutine = null;
    }

    private IEnumerator StartPatrol()
    {
        int index = 0;

        while (Waypoints.Count > 0)
        {
            // Set the destination for the agent
            Agent.destination = Waypoints[index];
            // Start searching for a path
            Agent.SearchPath();

            // Wait until we reach the end of the path or until path finding is not pending anymore
            while (!Agent.reachedEndOfPath || Agent.pathPending)
            {
                yield return null;
            }

            // Random delay before moving to the next waypoint
            float delay = Random.Range(DelayRange.x, DelayRange.y);
            yield return new WaitForSeconds(delay);

            // Move to the next waypoint, loop back to start if at the end
            index = (index + 1) % Waypoints.Count;
        }
    }

    public void SetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void ResetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (enabled == false || Waypoints == null || Waypoints.Count == 0)
            return;

        Gizmos.color = Color.cyan;
        Handles.color = Color.white;
        for (int i = 0; i < Waypoints.Count; i++)
        {
            Handles.DrawSolidDisc(Waypoints[i], Vector3.back, 0.2f);

            if (i > 0)
                Gizmos.DrawLine(Waypoints[i - 1], Waypoints[i]);

            if (i == Waypoints.Count - 1)
                Gizmos.DrawLine(Waypoints[i], Waypoints[0]);
        }
    }
#endif
}
