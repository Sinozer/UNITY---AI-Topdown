// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class FollowPlayerNode : ActionNode
{
    GameObject _self;
    EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.FollowingPlayer(true);
    }

    public override void OnStop()
    {

    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;


        return State.Success;
    }
}