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

    protected CustomPatrol CustomPatrol
    {
        get
        {
            if (_customPatrol == null)
                _customPatrol = Entity.GetComponentInChildren<CustomPatrol>();

            return _customPatrol;
        }
    }
    [SerializeField] private CustomPatrol _customPatrol;

    protected CustomDestinationSetter CustomDestinationSetter
    {
        get
        {
            if (_customDestinationSetter == null)
                _customDestinationSetter = Entity.GetComponentInChildren<CustomDestinationSetter>();

            return _customDestinationSetter;
        }
    }
    [SerializeField] private CustomDestinationSetter _customDestinationSetter;

    public Enemy Enemy => Entity as Enemy;

    protected virtual void Awake()
    {
        CustomPatrol.enabled = false;
        CustomDestinationSetter.enabled = false;
        AIPathfinder.enabled = true;
    }

    protected virtual void Start()
    {
        AIPathfinder.maxSpeed = Entity.MovementSpeed / 50f;
    }

    protected virtual void Update()
    {
        if (Dead)
        {
            Enemy.Rigidbody2D.simulated = false;
            return;
        }

        float testX = AIPathfinder.desiredVelocity.x;
        Player player = GameManager.Instance.Player;

        if (testX == 0 && player)
            testX = player.transform.position.x - transform.position.x;

        int degree = testX > 0 ? 0 : 180;

        Render.transform.rotation = Quaternion.Euler(0, degree, 0);
        Physics.transform.rotation = Quaternion.Euler(0, degree, 0);
    }

    public void FollowingPlayer(bool enable)
    {
        CustomDestinationSetter.enabled = enable;
    }

    public void Patrolling(bool enable)
    {
        CustomPatrol.enabled = enable;
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
        Entity.Attack(GameManager.Instance.Player);
    }

    public void OnHit()
    {
        Enemy.OnHit();
    }
}