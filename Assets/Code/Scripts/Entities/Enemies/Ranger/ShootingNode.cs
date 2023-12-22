// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ShootingNode : ActionNode
{
    EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("EnemyBrain", out _brain);
        if (_brain == null)
            return;
        
        _brain.StartShooting();
    }

    public override void OnStop()
    {
        _brain?.StopShooting();
    }

    public override State OnUpdate()
    {
        if (_brain == null)
            return State.Failure;

        if (_brain.CanShootAtPlayer)
            return State.Running;

        return State.Success;
    }
}