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

    protected void Awake()
    {
        base.Awake();
        _stateManager = new TankyStateManager(this);
    }

    private void Update()
    {
        _stateManager.Update();

        if (_entity.IsDead)
            return;

        _enemy.DistFromPlayer = _enemy.CalculateDistFromPlayer();

        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;
    }

    public void EndActivating()
    {
        _endActivating = true;
    }
}