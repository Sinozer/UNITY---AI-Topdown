// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class PatrollingNode : ActionNode
{
    private GameObject _self;
    private EnemyBrain _brain;

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
        if (_self == null)
            return State.Failure;

        if (_brain.IsInVisionRange == false)
            return State.Running;

        return State.Success;
    }
}