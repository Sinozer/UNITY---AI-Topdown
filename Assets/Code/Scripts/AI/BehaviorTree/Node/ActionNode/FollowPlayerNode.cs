// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class FollowPlayerNode : ActionNode
{
    private GameObject _self;
    private EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.FollowingPlayer(true);
        _brain.AIPath(true);
    }

    public override void OnStop()
    {
        _brain?.FollowingPlayer(false);
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;

        if(_brain.IsInShootRange)
            return State.Success;

        return State.Running;
    }
}