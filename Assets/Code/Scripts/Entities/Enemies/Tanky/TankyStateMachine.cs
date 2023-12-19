// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;
using static TankyStateManager;

public class TankyStateManager : BaseStateManager<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public enum ETankyState
    {
        Idle,
        Activating,
        IdleActivated,
        FollowingPlayer,
        Patrolling,
        Attacking,
        IsDead
    }

    private static Dictionary<ETankyState, BaseState<TankyStateManager, ETankyState, TankyBrain>> _states =
        new Dictionary<ETankyState, BaseState<TankyStateManager, ETankyState, TankyBrain>>
    {
        {ETankyState.Idle, new IdleState()},
        {ETankyState.Activating, new ActivatingState()},
        {ETankyState.IdleActivated, new IdleActivatedState()},
        {ETankyState.FollowingPlayer, new FollowingPlayerState()},
        {ETankyState.Patrolling, new PatrollingState()},
        {ETankyState.Attacking, new AttackingState()},
        {ETankyState.IsDead, new IsDeadState()}
    };
    public TankyStateManager(TankyBrain owner) : base(owner)
    {
        _currentState = _states[ETankyState.Idle];
        _currentState.OnEnter(this);
    }

    protected override BaseState<TankyStateManager, ETankyState, TankyBrain> GetState(ETankyState state)
    {
        return _states[state];
    }
}

public class IdleState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        //Debug.Log("Enter Idle");      
    }

    public override void OnExit(TankyStateManager manager)
    {
        //Debug.Log("Exit Idle");
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        //Debug.Log("Update Idle");

        if(manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }
        if (manager.Owner.SeePlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.Activating);
            return;
        }
    }
}
public class ActivatingState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        manager.Owner.Animator.SetBool(ETankyState.Activating.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        manager.Owner.Animator.SetBool(ETankyState.Activating.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        if (manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }

        if (manager.Owner.EndActivatingAnim)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IdleActivated);
            return;
        }
    }
}
public class IdleActivatedState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        manager.Owner.Animator.SetBool(ETankyState.IdleActivated.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        manager.Owner.Animator.SetBool(ETankyState.IdleActivated.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        if (manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }
        if (manager.Owner.CanShootAtPlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.Attacking);
            return;
        }
        if (manager.Owner.SeePlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.FollowingPlayer);
            return;
        }
    }
}

public class FollowingPlayerState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        manager.Owner.FollowingPlayer(true);
        manager.Owner.Animator.SetBool(ETankyState.FollowingPlayer.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        manager.Owner.FollowingPlayer(false);
        manager.Owner.Animator.SetBool(ETankyState.FollowingPlayer.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        if (manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }
        if (manager.Owner.CanShootAtPlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.Attacking);
            return;
        }
        if (!manager.Owner.SeePlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.Patrolling);
            return;
        }
    }
}

public class PatrollingState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        manager.Owner.Patrolling(true);
        manager.Owner.Animator.SetBool(ETankyState.Patrolling.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        manager.Owner.Patrolling(false);
        manager.Owner.Animator.SetBool(ETankyState.Patrolling.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        if (manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }
        if (manager.Owner.SeePlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.FollowingPlayer);
            manager.Owner.Animator.SetBool(ETankyState.FollowingPlayer.ToString(), true);
            return;
        }
    }
}

public class AttackingState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        manager.Owner.AIPath(false);
        manager.Owner.StartShooting();
        manager.Owner.Animator.SetBool(ETankyState.Attacking.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        manager.Owner.AIPath(true);
        manager.Owner.StopShooting();
        manager.Owner.Animator.SetBool(ETankyState.Attacking.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        if (manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }
        if (!manager.Owner.SeePlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.Patrolling);
            return;
        }
        if (!manager.Owner.CanShootAtPlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.FollowingPlayer);
            return;
        }
    }
}

public class IsDeadState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        manager.Owner.AIPath(false);
        manager.Owner.Animator.SetBool(ETankyState.IsDead.ToString(), true);
        manager.Owner.transform.parent.GetComponentInChildren<Collider2D>().enabled = false;
    }

    public override void OnExit(TankyStateManager manager)
    {
        manager.Owner.Animator.SetBool(ETankyState.IsDead.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {

    }
}