// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class IdleNode : ActionNode
{
    private GameObject _self;
    private EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.AIPath(false);
        
    }

    public override void OnStop()
    {
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.AIPath(true);
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;

        return State.Success;
    }
}