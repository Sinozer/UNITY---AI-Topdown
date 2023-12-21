// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class PatrollingNode : ActionNode
{
    GameObject _self;
    EnemyBrain _brain;

    public override void OnStart()
    {
        Debug.Log("Patrol start");
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _brain.FollowingPlayer(false);
        _brain.Patrolling(true);
        _brain.AIPath(true);
    }

    public override void OnStop()
    {
        Debug.Log("Follow stop");
        _brain.Patrolling(false);
        _brain.AIPath(false);
    }

    public override State OnUpdate()
    {
        //Debug.Log("Follow update");
        if (_self == null)
            return State.Failure;

        if (!_brain.CanShootAtPlayer)
        {
            _brain?.Patrolling(true);
            _brain?.AIPath(true);
            return State.Running;
        }

        Debug.Log("");
        return State.Success;

    }
}