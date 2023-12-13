// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;

public class BigRobotStateManager : BaseStateManager<BigRobotStateManager, BigRobotStateManager.EExampleState, BigRobotBrain>
{
    public enum EExampleState
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }

    private static Dictionary<EExampleState, BaseState<BigRobotStateManager, EExampleState, BigRobotBrain>> _states =
        new Dictionary<EExampleState, BaseState<BigRobotStateManager, EExampleState, BigRobotBrain>>
    {
        {EExampleState.Idle, new IdleState()}
    };
    public BigRobotStateManager(BigRobotBrain owner) : base(owner)
    {
        _currentState = _states[EExampleState.Idle];
        _currentState.OnEnter(this);
    }
    protected override BaseState<BigRobotStateManager, EExampleState, BigRobotBrain> GetState(EExampleState state)
    {
        return _states[state];
    }
}
public class IdleState : BaseState<BigRobotStateManager, BigRobotStateManager.EExampleState, BigRobotBrain>
{
    public override void OnEnter(BigRobotStateManager manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(BigRobotStateManager manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(BigRobotStateManager manager)
    {
        Debug.Log("Update Idle");
        //if (manager.Owner.IsIdle)
            manager.ChangeState(BigRobotStateManager.EExampleState.Moving);
    }
}