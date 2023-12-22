// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System;
using UnityEngine;

public class EnemyBrain : Brain
{
    public enum AnimatorCondition
    {
        Idle,
        Run,
        Attack,
        Hit,
        Dead
    }
    
    public bool IsDead => Entity.IsDead;
    public Action Die => Entity.Die;
    public float DistFromPlayer => _enemy.DistFromPlayer;
    
    
    public bool SeePlayer => _seePlayer;
    public bool CanShootAtPlayer => _canShootAtPlayer;
    public Animator Animator => _animator;

    [SerializeField] protected EntityShooting _entityShooting;
    [SerializeField] protected AIPath _aiPath;
    [SerializeField] protected CustomPatrol _customPatrol;
    [SerializeField] protected CustomDestinationSetter _customDestinationSetter;

    protected Enemy _enemy => Entity as Enemy;
    protected bool _seePlayer = false;
    protected bool _canShootAtPlayer = false;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = Render.GetComponent<Animator>();
        _customPatrol.enabled = false;
        _customDestinationSetter.enabled = false;
        _aiPath.enabled = true;
    }

    protected virtual void Start()
    {
        _aiPath.maxSpeed = Entity.MovementSpeed / 50f;
    }

    protected virtual void Update()
    {
        if (Entity.IsDead)
        {
            _enemy.Rigidbody.simulated = false;
            return;
        }

        _canShootAtPlayer = _enemy.DistFromPlayer < Entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < Entity.VisionRange;

        float testX = _aiPath.desiredVelocity.x;
        Player player = GameManager.Instance.Player;

        if (testX == 0 && player)
            testX = player.transform.position.x - transform.position.x;

        transform.root.rotation = Quaternion.Euler(0, testX > 0 ? 180 : 0, 0);
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
        _entityShooting.StartShooting();
    }

    public void StopShooting()
    {
        _entityShooting.StopShooting();
    }

    public void AttackPlayer()
    {
        Entity.Attack(GameManager.Instance.Player);
    }

    public void OnHit()
    {
        _enemy.OnHit();
    }

    public void PlayDeathSfx()
    {
        _enemy.PlayDeathSfx();
    }
}