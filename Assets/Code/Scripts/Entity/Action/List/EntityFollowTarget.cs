// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 30/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;

/// <summary>
/// Simple follow behavior.
/// This will set the destination on the agent so that it moves towards the <see cref="Target"/> object.
/// </summary>
public class EntityFollowTarget : EntityChild, IEntityAction
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
    public void SetAiDestination()
    {
        if (Target == null)
            return;

        Agent.destination = Target.position;
    }

    public void OnEnable()
    {
        Agent.onSearchPath += SetAiDestination;
    }
    public void OnDisable()
    {
        Agent.onSearchPath -= SetAiDestination;
    }

    private void Update()
    {
        SetAiDestination();
    }

    public void SetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void ResetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }
}