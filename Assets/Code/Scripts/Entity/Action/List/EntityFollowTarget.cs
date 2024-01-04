// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 30/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System.Collections;
using UnityEngine;

/// <summary>
/// Simple follow behavior.
/// This will set the destination on the agent so that it moves towards the <see cref="Target"/> object.
/// </summary>
public class EntityFollowTarget : EntityAction, IActionTargetable
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
    /// Target to follow
    /// </summary>
    public Transform Target { get; set; }

    /// <summary>
    /// Set the target position as the destination for the <see cref="Agent"/>
    /// </summary>
    protected override IEnumerator Action()
    {
        if (Target == null)
            yield break;

        Agent.destination = Target.position;

        yield return null;
    }

    public void OnEnable()
    {
        Agent.onSearchPath += StopAndExecute;
    }
    public void OnDisable()
    {
        Agent.onSearchPath -= StopAndExecute;
    }

    private void Update()
    {
        StopAndExecute();
    }
}