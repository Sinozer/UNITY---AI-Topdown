// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class MeleeAttackNode : ActionNode
{
    private GameObject _laserSaber;
    public override void OnStart()
    {
        if (!Blackboard.TryFind("LaserSaber", out _laserSaber))
            return;
        
        _laserSaber.SetActive(true);
    }

    public override void OnStop()
    {
        _laserSaber.SetActive(false);
    }

    public override State OnUpdate()
    {
        if (_laserSaber == null)
            return State.Failure;
        
        Blackboard.TryFind("CanMeleeAttack", out bool _canMeleeAttack);
        if (_canMeleeAttack)
        {
            return State.Running;
        }
        
        return State.Success;
    }
}