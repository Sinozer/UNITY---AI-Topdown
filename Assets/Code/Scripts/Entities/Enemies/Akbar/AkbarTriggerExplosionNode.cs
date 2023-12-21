// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AkbarTriggerExplosionNode : ActionNode
{
    GameObject _self;
    EnemyBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        Blackboard.TryFind("EnemyBrain", out _brain);
        _runner.GetBlackboard().SetValue("IsDead", _enemy.IsDead);
    }

    public override void OnStop()
    {

    }

    public override State OnUpdate()
    {

    }
}