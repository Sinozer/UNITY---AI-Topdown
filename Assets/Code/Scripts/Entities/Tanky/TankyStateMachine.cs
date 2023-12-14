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
        Moving,
        Attacking,
        IsDead
    }

    private static Dictionary<ETankyState, BaseState<TankyStateManager, ETankyState, TankyBrain>> _states =
        new Dictionary<ETankyState, BaseState<TankyStateManager, ETankyState, TankyBrain>>
    {
        {ETankyState.Idle, new IdleState()},
        {ETankyState.Activating, new ActivatingState()},
        {ETankyState.IdleActivated, new IdleActivatedState()},
        //{ETankyState.Moving, new MovingState()},
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
        } else if (manager.Owner.SeePlayer)
        {
            manager.ChangeState(TankyStateManager.ETankyState.Activating);
            manager.Owner.Animator.SetBool(ETankyState.Activating.ToString(), true);
        }
    }
}
public class ActivatingState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        //Debug.Log("Enter Activating");
    }

    public override void OnExit(TankyStateManager manager)
    {
        //Debug.Log("Exit Activating");
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        //Debug.Log("Update Activating");
        if (manager.Owner.IsDead)
        {
            manager.ChangeState(TankyStateManager.ETankyState.IsDead);
            return;
        }

        manager.ChangeState(TankyStateManager.ETankyState.IdleActivated);
        manager.Owner.Animator.SetBool(ETankyState.IdleActivated.ToString(), true);
    }
}
public class IdleActivatedState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        //Debug.Log("Enter IdleActivated");
        manager.Owner.Animator.SetBool(ETankyState.IdleActivated.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        //Debug.Log("Exit IdleActivated");
        manager.Owner.Animator.SetBool(ETankyState.IdleActivated.ToString(), false);
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
            manager.ChangeState(TankyStateManager.ETankyState.Attacking);
            manager.Owner.Animator.SetBool(ETankyState.Attacking.ToString(), true);
            return;
        }
    }
}
public class AttackingState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        //Debug.Log("Enter AttackingState");
        manager.Owner.Animator.SetBool(ETankyState.Attacking.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        //Debug.Log("Exit AttackingState");
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
            manager.ChangeState(TankyStateManager.ETankyState.IdleActivated);
            manager.Owner.Animator.SetBool(ETankyState.IdleActivated.ToString(), true);
            return;
        }
    }
}

public class IsDeadState : BaseState<TankyStateManager, TankyStateManager.ETankyState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        //Debug.Log("Enter IsDead");
        manager.Owner.Die(2);
        manager.Owner.Animator.SetBool(ETankyState.IsDead.ToString(), true);
    }

    public override void OnExit(TankyStateManager manager)
    {
        //Debug.Log("Exit IsDead");
        manager.Owner.Animator.SetBool(ETankyState.IsDead.ToString(), false);
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        //Debug.Log("Update IsDead");
    }
}