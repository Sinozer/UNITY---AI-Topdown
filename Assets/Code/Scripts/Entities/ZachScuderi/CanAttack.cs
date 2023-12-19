// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class CanAttack : DecoratorNode
{
    [SerializeField] private string _conditionName = "CanAttackCondition";

    private bool _isInRange = false;
    private float _attackRange;
    private float _distFromPlayer;
    public override void OnStart()
    {
        Blackboard.TryFind<float>("DistFromPlayer", out _distFromPlayer);
        Blackboard.TryFind<float>("AttackRange", out _attackRange);
        
        _isInRange = Mathf.Abs(_distFromPlayer) <= _attackRange;
    }

    public override void OnStop()
    {
        throw new System.NotImplementedException();
    }

    public override State OnUpdate()
    {
        if (_isInRange)
        {
            Child.Update();
            return State.Success;
        }
        return State.Failure;
    }
}