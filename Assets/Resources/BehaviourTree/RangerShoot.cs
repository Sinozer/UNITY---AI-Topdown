// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class RangerShoot : ActionNode
{
    private EnemyBrain _brain;
    private bool _activated = false;
    private bool _shooting = false;

    public override void OnStart()
    {
        if (!_activated)
        {
            _activated = Blackboard.TryFind("EnemyBrain", out _brain);
            return;
        }
        _shooting = true;
        _brain.StartShooting();
    }

    public override void OnStop()
    {
        if (!_activated)
        {
            _activated = Blackboard.TryFind("EnemyBrain", out _brain);
            return;
        }
        if (!_shooting) return;
        _brain.StopShooting();
        _shooting = false;
    }

    public override State OnUpdate()
    {
        if (_brain.CanShootAtPlayer)
            return State.Running;
        
        return State.Failure;
    }
}
