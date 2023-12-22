// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class SprintToPlayerNode : ActionNode
{
    private GameObject _self;
    private EnemyBrain _brain;
    public override void OnStart()
    {
        if (!Blackboard.TryFind("Self", out _self))
            return;
        
        if (!Blackboard.TryFind("EnemyBrain", out _brain))
            return;
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;

        if (_brain.CanShootAtPlayer)
            return State.Success;
        
       
        return State.Running;
    }
}