// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;
using static ExampleStateManager;

public class LevelStateMachine : BaseStateManager<LevelStateMachine, LevelStateMachine.LevelState, LevelManager>
{
    public enum LevelState
    {
        StartLevel,
        NextRoom,
        RoomCleared,
        EndLevel,
    }
    private static Dictionary<LevelState, BaseState<LevelStateMachine, LevelState, LevelManager>> _levelStates =
    new Dictionary<LevelState, BaseState<LevelStateMachine, LevelState, LevelManager>>
{
        {LevelState.StartLevel, new StartLevelState()},
        {LevelState.NextRoom, new NextRoomState()},
        {LevelState.RoomCleared, new RoomClearedState()},
        {LevelState.EndLevel, new EndLevelState()}
};
    public LevelStateMachine(LevelManager owner) : base(owner)
    {
    }

    protected override BaseState<LevelStateMachine, LevelState, LevelManager> GetState(LevelState state)
    {
        return _levelStates[state];
    }
}

public class StartLevelState : BaseState<LevelStateMachine, LevelStateMachine.LevelState, LevelManager>
{
    public override void OnEnter(LevelStateMachine manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(LevelStateMachine manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(LevelStateMachine manager)
    {
        Debug.Log("Update Idle");
    }
}

public class NextRoomState : BaseState<LevelStateMachine, LevelStateMachine.LevelState, LevelManager>
{
    public override void OnEnter(LevelStateMachine manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(LevelStateMachine manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(LevelStateMachine manager)
    {
        Debug.Log("Update Idle");
    }
}

public class RoomClearedState : BaseState<LevelStateMachine, LevelStateMachine.LevelState, LevelManager>
{
    public override void OnEnter(LevelStateMachine manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(LevelStateMachine manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(LevelStateMachine manager)
    {
        Debug.Log("Update Idle");
    }
}

public class EndLevelState : BaseState<LevelStateMachine, LevelStateMachine.LevelState, LevelManager>
{
    public override void OnEnter(LevelStateMachine manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(LevelStateMachine manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(LevelStateMachine manager)
    {
        Debug.Log("Update Idle");
    }
}