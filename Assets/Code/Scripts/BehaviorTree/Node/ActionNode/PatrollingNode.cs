// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class PatrollingNode : ActionNode
{
    GameObject _self;
    EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.Patrolling(true);
        _brain.AIPath(true);
    }

    public override void OnStop()
    {
        _brain?.Patrolling(false);
    }

    public override State OnUpdate()
    {
        //Debug.Log("Follow update");
        if (_self == null)
            return State.Failure;

        if (!_brain.SeePlayer)
            return State.Running;

        return State.Success;
    }
}