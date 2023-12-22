// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class RangerShoot : ActionNode
{
    private RangerBrain _brain;
    private bool _activated = false;
    private bool _shooting = false;

    public override void OnStart()
    {
        if (!_activated)
        {
            _activated = Blackboard.TryFind<RangerBrain>("EnemyBrain", out _brain);
            return;
        }
        _shooting = true;
        _brain.Animator.SetBool("IsShooting", true);
        _brain.StartShooting();
    }

    public override void OnStop()
    {
        if (!_activated)
        {
            _activated = Blackboard.TryFind<RangerBrain>("EnemyBrain", out _brain);
            return;
        }
        if (!_shooting) return;
        _brain.StopShooting();
        _brain.Animator.SetBool("IsShooting", false);
        _shooting = false;
    }

    public override State OnUpdate()
    {
        if (_brain.CanShootAtPlayer)
            return State.Running;
        
        return State.Failure;
    }
}
