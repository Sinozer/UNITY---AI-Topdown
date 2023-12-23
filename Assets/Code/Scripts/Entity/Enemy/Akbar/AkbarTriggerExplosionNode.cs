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

        if (_brain.Enemy.DistFromPlayer < _brain.ExplosionRange)
            _brain.Enemy.Attack(_brain.Player);

        _brain.Enemy.TakeDamage(_brain.Enemy.MaxHealth);
        Blackboard.SetValue("TriggerExplosion", true);
        _brain.FxExplosion.Invoke();
        _brain.ExplosionSound.Play();
    }

    public override void OnStop()
    {

    }

    public override State OnUpdate()
    {
        return State.Success;
    }
}