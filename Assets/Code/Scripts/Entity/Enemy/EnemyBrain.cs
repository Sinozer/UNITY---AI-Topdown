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
    public Action Die => Entity.Die;
    public bool IsInVisionRange => Enemy.DistFromPlayer < Entity.VisionRange;
    public bool IsInShootRange => Enemy.DistFromPlayer < Entity.AttackRange;


    [SerializeField] protected EnemyBTRunner _btRunner;

    [SerializeField] protected EntityShoot _entityShooting;
    [SerializeField] protected AIPath _aiPath;
    [SerializeField] protected CustomPatrol _customPatrol;
    [SerializeField] protected CustomDestinationSetter _customDestinationSetter;

    public Enemy Enemy => Entity as Enemy;

    protected virtual void Awake()
    {
        if (_btRunner == null)
            _btRunner = transform.root.GetComponentInChildren<EnemyBTRunner>();

        if (_entityShooting == null)
            _entityShooting = transform.root.GetComponentInChildren<EntityShoot>();

        if (_aiPath == null)
            _aiPath = transform.root.GetComponentInChildren<AIPath>();

        if (_customPatrol == null)
            _customPatrol = transform.root.GetComponentInChildren<CustomPatrol>();

        if (_customDestinationSetter == null)
            _customDestinationSetter = transform.root.GetComponentInChildren<CustomDestinationSetter>();

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
        if (Dead)
        {
            Enemy.Rigidbody.simulated = false;
            return;
        }

        float testX = _aiPath.desiredVelocity.x;
        Player player = GameManager.Instance.Player;

        if (testX == 0 && player)
            testX = player.transform.position.x - transform.position.x;

        int degree = testX > 0 ? 0 : 180;

        Render.transform.rotation = Quaternion.Euler(0, degree, 0);
        Physics.transform.rotation = Quaternion.Euler(0, degree, 0);
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
        Enemy.OnHit();
    }
}