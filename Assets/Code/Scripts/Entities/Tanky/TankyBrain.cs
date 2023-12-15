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
    [SerializeField] private GameObject _followPlayerScripts;
    [SerializeField] private bool _seePlayer = false;
    [SerializeField] private bool _canShootAtPlayer = false;

    private bool _endActivating = false;
    private TankyStateManager _stateManager;
    private Animator _animator;

    private void Awake()
    {
        _stateManager = new TankyStateManager(this);
        _animator = _render.GetComponent<Animator>();

        _followPlayerScripts.GetComponent<AIPath>().maxSpeed = _movementSpeed;
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

    public void StopFollowingPlayer()
    {
        _followPlayerScripts.SetActive(false);
    }

    public void StartFollowingPlayer()
    {
        _followPlayerScripts.SetActive(true);
    }

    public void EndActivating()
    {
        _endActivating = true;
    }
}