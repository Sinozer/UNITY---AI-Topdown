using UnityEngine;
using Pathfinding;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Simple patrol behavior.
/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="targets"/> array.
/// Upon reaching a target it will wait for <see cref="delay"/> seconds.
/// </summary>
public class CustomPatrol : MonoBehaviour {
    /// <summary>Target points to move to in order</summary>
    public Vector3[] targets;

    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;

    /// <summary>Current target index</summary>
    int index;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    private void Awake () {
        agent = GetComponent<IAstarAI>();
    }

    /// <summary>Update is called once per frame</summary>
    private void Update () {
        if (targets.Length == 0) return;

        bool search = false;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime)) {
            switchTime = Time.time + delay;
        }

        if (Time.time >= switchTime) {
            index = index + 1;
            search = true;
            switchTime = float.PositiveInfinity;
        }

        index = index % targets.Length;
        agent.destination = targets[index];

        if (search) agent.SearchPath();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (enabled == false || targets == null || targets.Length == 0)
            return;

        Gizmos.color = Color.cyan; 
        Handles.color = Color.white;
        for (int i = 0; i < targets.Length; i++)
        {
            Handles.DrawWireDisc(targets[i], Vector3.back, 0.1f);
        }

        // draw lines between targets
        for (int i = 0; i < targets.Length; i++)
        {
            if (i == targets.Length - 1)
                Gizmos.DrawLine(targets[i], targets[0]);
            else
                Gizmos.DrawLine(targets[i], targets[i + 1]);
        }
    }
#endif
}
