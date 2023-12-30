// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AttackPlayerNode : ActionNode
{
    private GameObject _self;
    private EnemyBrain _brain;
    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain?.StartShooting();
    }

    public override void OnStop()
    {
        _brain?.StopShooting();
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;
        
        if (!_brain.IsInShootRange) return State.Failure;
        
        _brain.AttackPlayer();

        return State.Success;
    }
}