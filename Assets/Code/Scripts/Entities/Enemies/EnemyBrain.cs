// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public bool IsDead => _entity.IsDead;
    public Action Die => _entity.Die;
    public bool SeePlayer => _seePlayer;
    public bool CanShootAtPlayer => _canShootAtPlayer;
    public Animator Animator => _animator;

    [SerializeField] protected Entity _entity;
    [SerializeField] protected EntityShooting _entityShooting;
    [SerializeField] protected GameObject _render;
    [SerializeField] protected AIPath _aiPath;
    [SerializeField] protected CustomPatrol _customPatrol;
    [SerializeField] protected CustomDestinationSetter _customDestinationSetter;

    protected Enemy _enemy => _entity as Enemy;
    protected bool _seePlayer = false;
    protected bool _canShootAtPlayer = false;
    protected Animator _animator;
    protected virtual void Awake()
    {
        _entity = GetComponentInParent<Entity>();

        _animator = _render.GetComponent<Animator>();
        _customPatrol.enabled = false;
        _customDestinationSetter.enabled = false;
        _aiPath.enabled = true;
    }

    protected virtual void Start()
    {
        _aiPath.maxSpeed = _entity.MovementSpeed;
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

    public void StartShooting()
    {
        _entityShooting.StartShooting(_entity.AttackSpeed);
    }

    public void StopShooting()
    {
        _entityShooting.StopShooting();
    }
}