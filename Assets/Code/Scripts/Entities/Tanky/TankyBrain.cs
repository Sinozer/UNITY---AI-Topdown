// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System;
using UnityEngine;

public class TankyBrain : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    private Enemy _enemy => _entity as Enemy;

    public bool IsDead => _entity.IsDead;

    public Action<float> Die => _entity.Die;

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

    protected void Awake()
    {
        _entity = GetComponentInParent<Entity>();

        _stateManager = new TankyStateManager(this);
        _animator = _render.GetComponent<Animator>();

        _pathFinder.GetComponent<AIPath>().maxSpeed = _entity.MovementSpeed;
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