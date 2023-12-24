// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class ShootingNode : ActionNode
{
    private EnemyBrain _brain;

    public override void OnStart()
    {
        if (Blackboard.TryFind("EnemyBrain", out _brain) == false)
            return;
        
        _brain.StartShooting();
    }

    public override void OnStop()
    {
        if (Blackboard.TryFind("EnemyBrain", out _brain) == false)
            return;

        _brain.StopShooting();
    }

    public override State OnUpdate()
    {
        if (_brain == null)
            return State.Failure;

        //if (_brain.CanShootAtPlayer)
        //    return State.Running;

        return State.Success;
    }
}