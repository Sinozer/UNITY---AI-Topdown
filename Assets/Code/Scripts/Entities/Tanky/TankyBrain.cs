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
    public bool IsDead => _entity.IsDead;
    public Action Die => _entity.Die;
    public bool EndActivatingAnim => _endActivating;
    public bool SeePlayer => _seePlayer;
    public bool CanShootAtPlayer => _canShootAtPlayer;
    public Animator Animator => _animator;

    [SerializeField] private Entity _entity;
    [SerializeField] private EntityShooting _entityShooting;
    [SerializeField] private GameObject _render;
    [SerializeField] private AIPath _aiPath;
    [SerializeField] private CustomPatrol _customPatrol;
    [SerializeField] private CustomDestinationSetter _customDestinationSetter;

    private Enemy _enemy => _entity as Enemy;
    private bool _seePlayer = false;
    private bool _canShootAtPlayer = false;
    private bool _endActivating = false;
    private TankyStateManager _stateManager;
    private Animator _animator;

    protected void Awake()
    {
        _entity = GetComponentInParent<Entity>();

        _stateManager = new TankyStateManager(this);
        _animator = _render.GetComponent<Animator>();
        _customPatrol.enabled = false;
        _customDestinationSetter.enabled = false;
        _aiPath.enabled = true;
    }

    private void Start()
    {
        _aiPath.maxSpeed = _entity.MovementSpeed;
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

    public void FollowingPlayer(bool enable)
    {
        _customDestinationSetter.enabled = enable;
    }

    public void Patrolling(bool enable)
    {
        _customPatrol.enabled = enable;
    }

    public void AIPath(bool enable)
    {
        _aiPath.enabled = enable;
    }

    public void EndActivating()
    {
        _endActivating = true;
    }

    public void StartShooting()
    {
        _entityShooting.StartShooting(_entity.AttackSpeed);
    }

    public void StopShooting()
    {
        _entityShooting.StopShooting();
    }
}