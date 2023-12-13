// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;

public class TankyStateManager : BaseStateManager<TankyStateManager, TankyStateManager.EExampleState, TankyBrain>
{
    public enum EExampleState
    {
        Idle,
        Activating,
        IdleActivated,
        Moving,
        Attacking,
        Dead
    }

    private static Dictionary<EExampleState, BaseState<TankyStateManager, EExampleState, TankyBrain>> _states =
        new Dictionary<EExampleState, BaseState<TankyStateManager, EExampleState, TankyBrain>>
    {
        {EExampleState.Idle, new IdleState()},
        {EExampleState.Activating, new ActivatingState()},
        {EExampleState.IdleActivated, new IdleActivatedState()}
    };
    public TankyStateManager(TankyBrain owner) : base(owner)
    {
        _currentState = _states[EExampleState.Idle];
        _currentState.OnEnter(this);
    }
    protected override BaseState<TankyStateManager, EExampleState, TankyBrain> GetState(EExampleState state)
    {
        return _states[state];
    }
}
public class IdleState : BaseState<TankyStateManager, TankyStateManager.EExampleState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(TankyStateManager manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        Debug.Log("Update Idle");
        if (manager.Owner.SeePlayer)
            manager.ChangeState(TankyStateManager.EExampleState.Activating);
    }
}
public class ActivatingState : BaseState<TankyStateManager, TankyStateManager.EExampleState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        Debug.Log("Enter Activating");
    }

    public override void OnExit(TankyStateManager manager)
    {
        Debug.Log("Exit Activating");
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        Debug.Log("Update Activating");
        if (manager.Owner.SeePlayer)
            manager.ChangeState(TankyStateManager.EExampleState.IdleActivated);
    }
}
public class IdleActivatedState : BaseState<TankyStateManager, TankyStateManager.EExampleState, TankyBrain>
{
    public override void OnEnter(TankyStateManager manager)
    {
        Debug.Log("Enter IdleActivated");
    }

    public override void OnExit(TankyStateManager manager)
    {
        Debug.Log("Exit IdleActivated");
    }

    public override void OnUpdate(TankyStateManager manager)
    {
        Debug.Log("Update IdleActivated");
        //if (manager.Owner.SeePlayer)
        //    manager.ChangeState(TankyStateManager.EExampleState.IdleActivated);
    }
}