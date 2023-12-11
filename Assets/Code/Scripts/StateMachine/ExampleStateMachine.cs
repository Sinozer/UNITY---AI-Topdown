// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 07/12/23
//  Author: guill
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;

public class ExampleStateManager : BaseStateManager<ExampleStateManager, ExampleStateManager.EExampleState, ExampleObj>
{
    public enum EExampleState
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }

    private static Dictionary<EExampleState, BaseState<ExampleStateManager, EExampleState, ExampleObj>> _states = 
        new Dictionary<EExampleState, BaseState<ExampleStateManager, EExampleState, ExampleObj>>
    {
        {EExampleState.Idle, new ExampleIdleState()},
        {EExampleState.Moving, new ExampleMoveState()},
        {EExampleState.Attacking, new ExampleAttackState()},
        {EExampleState.Dead, new ExampleDeadState()}
    };

    public ExampleStateManager(ExampleObj owner) : base(owner) 
    {
        _currentState = _states[EExampleState.Idle];
        _currentState.OnEnter(this);
    }

    public ExampleStateManager(ExampleObj owner, EExampleState state) : base(owner)
    {
        _currentState = _states[state];
        _currentState.OnEnter(this);
    }

    protected override BaseState<ExampleStateManager, EExampleState, ExampleObj> GetState(EExampleState state)
    {
        return _states[state];
    }
}

////////////////////////////////////////////////////////////////////////////////
/// ExampleIdleState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class ExampleIdleState : BaseState<ExampleStateManager, ExampleStateManager.EExampleState, ExampleObj>
{
    public override void OnEnter(ExampleStateManager manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(ExampleStateManager manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(ExampleStateManager manager)
    {
        Debug.Log("Update Idle");
        if (manager.Owner.IsIdle)
            manager.ChangeState(ExampleStateManager.EExampleState.Moving);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// ExampleMoveState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class ExampleMoveState : BaseState<ExampleStateManager, ExampleStateManager.EExampleState, ExampleObj>
{
    public override void OnEnter(ExampleStateManager manager)
    {
        Debug.Log("Enter Move");
    }

    public override void OnExit(ExampleStateManager manager)
    {
        Debug.Log("Exit Move");
    }

    public override void OnUpdate(ExampleStateManager manager)
    {
        manager.Owner.transform.position += new Vector3(10f * Time.deltaTime, 0f, 0f);

        Debug.Log(manager.Owner.transform.position.x);
        if (manager.Owner.transform.position.x > 10f)
            manager.ChangeState(ExampleStateManager.EExampleState.Attacking);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// ExampleAttackState /////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class ExampleAttackState : BaseState<ExampleStateManager, ExampleStateManager.EExampleState, ExampleObj>
{
    public override void OnEnter(ExampleStateManager manager)
    {
        Debug.Log("Enter Attack");
    }

    public override void OnExit(ExampleStateManager manager)
    {
        Debug.Log("Exit Attack");
    }

    public override void OnUpdate(ExampleStateManager manager)
    {
        Debug.Log("Update Attack");
        manager.Owner.AttackCount++;
        if (manager.Owner.AttackCount > 10)
        {
            manager.Owner.AttackCount = 0;
            manager.ChangeState(ExampleStateManager.EExampleState.Dead);
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
/// ExampleDeadState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class ExampleDeadState : BaseState<ExampleStateManager, ExampleStateManager.EExampleState, ExampleObj>
{
    public override void OnEnter(ExampleStateManager manager)
    {
        Debug.Log("Enter Dead");
        manager.Owner.IsDead = true;
    }

    public override void OnExit(ExampleStateManager manager)
    {
        Debug.Log("Exit Dead");
    }

    public override void OnUpdate(ExampleStateManager manager)
    {
        Debug.Log("Update Dead");
    }
}
