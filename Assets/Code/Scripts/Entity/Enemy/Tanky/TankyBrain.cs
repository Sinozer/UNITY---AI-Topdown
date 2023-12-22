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
    }

    public void EndActivating()
    {
        _endActivating = true;
    }
}