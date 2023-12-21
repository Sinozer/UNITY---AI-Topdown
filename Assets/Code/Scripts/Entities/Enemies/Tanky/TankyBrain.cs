// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System;
using UnityEngine;

public class TankyBrain : EnemyBrain
{
    public bool EndActivatingAnim => _endActivating;

    private bool _endActivating = false;
    private TankyStateManager _stateManager;

    protected override void Awake()
    {
        base.Awake();
        _stateManager = new TankyStateManager(this);
    }

    protected override void Update()
    {
        base.Update();
        _stateManager.Update();

        if (_entity.IsDead)
            return;

        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;
    }

    public void EndActivating()
    {
        _endActivating = true;
    }
}