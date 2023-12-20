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
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
    }

    public override void OnStop()
    {
    }

    public override State OnUpdate()
    {
        //Debug.Log("Attack update");
        if (!_brain.CanShootAtPlayer)
            return State.Failure;

        return State.Running;
    }
}