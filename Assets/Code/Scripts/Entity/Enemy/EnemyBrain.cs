// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System;
using UnityEngine;

public class EnemyBrain : EntityBrain
{
    public bool IsInVisionRange => Enemy.DistFromPlayer < Entity.VisionRange;
    public bool IsInShootRange => Enemy.DistFromPlayer < Entity.AttackRange;

    protected EnemyBTRunner BTRunner
    {
        get
        {
            if (_btRunner == null)
                _btRunner = Entity.GetComponentInChildren<EnemyBTRunner>();

            return _btRunner;
        }
    }
    [SerializeField] private EnemyBTRunner _btRunner;

    protected AIPath AIPathfinder
    {
        get
        {
            if (_aiPathfinder == null)
                _aiPathfinder = Entity.GetComponentInChildren<AIPath>();

            return _aiPathfinder;
        }
    }
    [SerializeField] private AIPath _aiPathfinder;

    protected EntityPatrol ActionPatrol
    {
        get
        {
            if (_actionPatrol == null)
                _actionPatrol = Entity.GetComponentInChildren<EntityPatrol>();

            return _actionPatrol;
        }
    }
    [SerializeField] private EntityPatrol _actionPatrol;

    protected EntityFollowTarget ActionFollowTarget
    {
        get
        {
            if (_actionFollowTarget == null)
                _actionFollowTarget = GetAction<EntityFollowTarget>();

            return _actionFollowTarget;
        }
    }
    [SerializeField] private EntityFollowTarget _actionFollowTarget;

    public Enemy Enemy => Entity as Enemy;

    public Player Player
    {
        get
        {
            if (_player == null)
                _player = GameManager.Instance.Player;

            return _player;
        }
    }
    private Player _player;

    protected virtual void Awake()
    {
        ActionPatrol.enabled = false;
        ActionFollowTarget.enabled = false;
        AIPathfinder.enabled = true;
    }

    protected virtual void Start()
    {
        AIPathfinder.maxSpeed = Entity.MovementSpeed / 50f;

        EntityLookAt lookAtAction = GetAction<EntityLookAt>();

        if (lookAtAction)
            lookAtAction.Target = GameManager.Instance.Player?.transform;
    }

    protected virtual void Update()
    {
        if (Dead)
        {
            Enemy.Rigidbody2D.simulated = false;
            return;
        }

        //float testX = AIPathfinder.desiredVelocity.x;
        //Player player = GameManager.Instance.Player;

        //if (testX == 0 && player)
        //    testX = player.transform.position.x - transform.position.x;

        //int degree = testX > 0 ? 0 : 180;

        //Render.transform.rotation = Quaternion.Euler(0, degree, 0);
        //Physics.transform.rotation = Quaternion.Euler(0, degree, 0);
    }

    public void FollowingPlayer(bool enable)
    {
        ActionFollowTarget.Target = GameManager.Instance.Player.transform;
        ActionFollowTarget.enabled = enable;
    }

    public void Patrolling(bool enable)
    {
        ActionPatrol.enabled = enable;
    }

    public void AIPath(bool enable)
    {
        AIPathfinder.enabled = enable;
    }

    public void StartShooting()
    {
        ShootAction.StartShooting();
    }

    public void StopShooting()
    {
        ShootAction.StopShooting();
    }

    public void AttackPlayer()
    {
        Entity.Attack(Player);
    }

    public void OnHit()
    {
        Enemy.OnHit();
    }
}