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
    public bool SeePlayer => _seePlayer;
    public Animator Animator => _animator;

    [SerializeField] private GameObject _render;
    [SerializeField] private AIPath _aiPath;
    [SerializeField] private CustomDestinationSetter _followPlayerScript;
    [SerializeField] private bool _seePlayer = false;

    private TankyStateManager _stateManager;
    private Animator _animator;


    private void Awake()
    {
        _health = 150;
        _movementSpeed = 0.6f;
        _attackSpeed = 1.1f;
        _damage = 6;
        _attackRange = 10;

        _stateManager = new TankyStateManager(this);
        _animator = _render.GetComponent<Animator>();

        _aiPath.maxSpeed = _movementSpeed;
    }

    private void Update()
    {
        _stateManager.Update();

        if (IsDead)
            return;

        _distFromPlayer = CalculateDistFromPlayer();
        if (_distFromPlayer < _attackRange)
            _seePlayer = true;
        else 
            _seePlayer = false;

    }

    public void StopFollowingPlayer()
    {
        _followPlayerScript.enabled = false;
    }

    public void StartFollowingPlayer()
    {
        _followPlayerScript.enabled = true;
    }
}