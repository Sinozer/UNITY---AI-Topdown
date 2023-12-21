// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AttackPlayerNode : ActionNode
{
    GameObject _self;
    EnemyBrain _brain;
    public override void OnStart()
    {
        Debug.Log("Attack start");
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
    }

    public override void OnStop()
    {
        Debug.Log("Attack stop");
    }

    public override State OnUpdate()
    {
        
        if (_self == null)
            return State.Failure;
        
        if (!_brain.CanShootAtPlayer) return State.Failure;
        
        _brain.AttackPlayer();

        return State.Success;
    }
}