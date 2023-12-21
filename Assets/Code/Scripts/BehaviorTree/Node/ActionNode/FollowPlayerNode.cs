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
        //Debug.Log("Follow start");
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.FollowingPlayer(true);
        _brain.AIPath(true);
    }

    public override void OnStop()
    {
        //Debug.Log("Follow stop");
        _brain?.FollowingPlayer(false);
        _brain?.AIPath(false);
    }

    public override State OnUpdate()
    {
        //Debug.Log("Follow update");
        if (_self == null)
            return State.Failure;

        if(_brain.CanShootAtPlayer)
            return State.Success;

        return State.Running;
    }
}