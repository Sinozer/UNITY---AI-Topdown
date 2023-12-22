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
    public enum AnimatorCondition
    {
        Idle,
        Run,
        Attack,
        Hit,
        Dead
    }
    
    public Action Die => Entity.Die;
    public bool SeePlayer => Enemy.DistFromPlayer < Entity.VisionRange;
    public bool CanShootAtPlayer => Enemy.DistFromPlayer < Entity.AttackRange;
    
    
    [SerializeField] protected EntityShoot _entityShooting;
    [SerializeField] protected AIPath _aiPath;
    [SerializeField] protected CustomPatrol _customPatrol;
    [SerializeField] protected CustomDestinationSetter _customDestinationSetter;

    public Enemy Enemy => Entity as Enemy;

    protected virtual void Awake()
    {
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

    public void PlayDeathSfx()
    {
        Enemy.PlayDeathSfx();
    }
}