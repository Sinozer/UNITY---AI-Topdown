// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AkbarTriggerExplosionNode : ActionNode
{
    AkbarBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("EnemyBrain", out _brain);
        Blackboard.SetValue("TriggerExplosion", true);
        _brain.Explode();
    }

    public override void OnStop()
    {

    }

    public override State OnUpdate()
    {
        return State.Success;
    }
}