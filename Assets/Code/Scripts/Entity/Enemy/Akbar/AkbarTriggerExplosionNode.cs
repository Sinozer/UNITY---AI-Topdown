// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class AkbarTriggerExplosionNode : ActionNode
{
    private AkbarBrain _brain;

    public override void OnStart()
    {
        Blackboard.TryFind("EnemyBrain", out _brain);

        _brain.Enemy.Attack(_brain.Player);
        _brain.Enemy.TakeDamage(_brain.Enemy.Data.GetValue<float>("MaxHealth"));

        _brain.VFXManager.PlayVFX("Explode");
        _brain.AudioManager.PlaySFX("Explode");
    }

    public override void OnStop()
    {

    }

    public override State OnUpdate()
    {
        return State.Success;
    }
}