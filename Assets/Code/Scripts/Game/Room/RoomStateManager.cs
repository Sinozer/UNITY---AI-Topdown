// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class RoomStateManager : BaseStateManager<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public enum ERoomState
    {
        Locked,         // Room is locked, player cannot enter
        Unlocked,       // Room is unlocked, waiting for player to enter
        Enter,          // Player has entered the room
        Setup,          // Room is being setup for play
        Play,           // Game is in progress in this room
        End,            // Room is cleared
        Leave,          // Player has left the room
    }

    private static readonly BaseState<RoomStateManager, ERoomState, Room>[] _states =
    {
        new RoomLockedState(),
        new RoomIdleState(),
        new RoomEnterState(),
        new RoomSetupState(),
        new RoomPlayState(),
        new RoomEndState(),
        new RoomLeaveState(),
    };

    public RoomStateManager(Room owner) : base(owner)
    {
        _currentState = _states[(int)ERoomState.Locked];
        _currentState.OnEnter(this);
    }

    public RoomStateManager(Room owner, ERoomState state) : base(owner)
    {
        _currentState = _states[(int)state];
        _currentState.OnEnter(this);
    }

    protected override BaseState<RoomStateManager, ERoomState, Room> GetState(ERoomState state)
    {
        return _states[(int)state];
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomLockedState ////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomLockedState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Locked");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Locked");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Locked");
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomIdleState //////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomIdleState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Idle");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Idle");
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomEnterState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomEnterState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Enter");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Enter");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Enter");
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomSetupState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomSetupState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Setup");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Setup");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Setup");
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomPlayState //////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomPlayState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Play");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Play");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Play");
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomEndState ///////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomEndState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter End");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit End");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update End");
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomLeaveState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomLeaveState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Leave");
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Leave");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Leave");
    }
}