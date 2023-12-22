// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class IdleNode : ActionNode
{
    GameObject _self;
    EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.AIPath(false);
        
    }

    public override void OnStop()
    {
        _brain?.AIPath(true);
    }

    public override State OnUpdate()
    {
        //Debug.Log("Idle update");
        if (_self == null)
            return State.Failure;

        return State.Success;
    }
}