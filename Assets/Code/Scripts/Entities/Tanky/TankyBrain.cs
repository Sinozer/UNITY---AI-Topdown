// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System;
using UnityEngine;

public class TankyBrain : Enemy
{
    public bool EndActivatingAnim => _endActivating;
    public bool SeePlayer => _seePlayer;
    public bool CanShootAtPlayer => _canShootAtPlayer;
    public Animator Animator => _animator;

    [SerializeField] private GameObject _render;
    [SerializeField] private GameObject _pathFinder;
    [SerializeField] private bool _seePlayer = false;
    [SerializeField] private bool _canShootAtPlayer = false;

    private bool _endActivating = false;
    private TankyStateManager _stateManager;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _stateManager = new TankyStateManager(this);
        _animator = _render.GetComponent<Animator>();

        _pathFinder.GetComponent<AIPath>().maxSpeed = _movementSpeed;
    }

    private void Update()
    {
        _stateManager.Update();

        if (IsDead)
            return;

        _distFromPlayer = CalculateDistFromPlayer();

        _canShootAtPlayer = _distFromPlayer < _attackRange ? true : false;
        _seePlayer = _distFromPlayer < _visionRange ? true : false;
    }

    public void StartFollowingPlayer()
    {
        _pathFinder.GetComponent<CustomDestinationSetter>().enabled = true;
    }

    public void StopFollowingPlayer()
    {
        _pathFinder.GetComponent<CustomDestinationSetter>().enabled = false;
    }

    public void StartPatrolling()
    {
        _pathFinder.GetComponent<CustomPatrol>().enabled = true;
    }

    public void StopPatrolling()
    {
        _pathFinder.GetComponent<CustomPatrol>().enabled = false;
    }

    public void EnableAIPath()
    {
        _pathFinder.GetComponent<AIPath>().enabled = true;
    }

    public void DisableAIPath()
    {
        _pathFinder.GetComponent<AIPath>().enabled = false;
    }

    public void EndActivating()
    {
        _endActivating = true;
    }
}