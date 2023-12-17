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
        new RoomUnlockedState(),
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
    private static void CheckLocked(RoomStateManager manager)
    {
        if (manager.Owner.IsLocked == true)
            return;

        manager.ChangeState(RoomStateManager.ERoomState.Unlocked);
        return;
    }

    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Locked");

        foreach (var gate in manager.Owner.Gates)
        {
            gate.SetActive(true);
        }
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Locked");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Locked");
        CheckLocked(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomUnlockedState //////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomUnlockedState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    private void CheckEntered(RoomStateManager manager)
    {
        if (manager.Owner.IsPlayerInside == false)
            return;

        manager.ChangeState(RoomStateManager.ERoomState.Enter);
        return;
    }

    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Unlocked");

        foreach (var gate in manager.Owner.Gates)
        {
            gate.SetActive(false);
        }

        if (manager.Owner.RoomType != Room.ERoomType.Join)
            return;

        GameObject.Instantiate(
            ((JoinRoom)manager.Owner).PlayerPrefab,
            manager.Owner.transform.position,
            Quaternion.identity
        ).name = "Player";

        manager.ChangeState(RoomStateManager.ERoomState.Enter);
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Unlocked");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Unlocked");
        CheckEntered(manager);
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

        manager.Owner.HasBeenEntered = true;
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Enter");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Enter");

        switch (manager.Owner.RoomType)
        {
            case Room.ERoomType.Join:
            case Room.ERoomType.Idle:
            case Room.ERoomType.Combat:
            case Room.ERoomType.Treasure:
            case Room.ERoomType.Boss:
            case Room.ERoomType.End:
                // Bypass this state, for now he's not that useful
                manager.ChangeState(RoomStateManager.ERoomState.Setup);
                break;
        }
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

        // Do things to setup the room based on the room type

        switch (manager.Owner.RoomType)
        {
            case Room.ERoomType.Join:
                // Spawn the player at the right place
                // Show player inputs instructions / base mechanics
                break;
            case Room.ERoomType.Idle:
                // Pretty much nothing to do here
                break;
            case Room.ERoomType.Combat:
                // Spawn enemies
                break;
            case Room.ERoomType.Treasure:
                // Spawn treasures
                break;
            case Room.ERoomType.Boss:
                // Spawn boss
                break;
            case Room.ERoomType.End:
                // Show end level screen
                break;
        }

        foreach (var gate in manager.Owner.Gates)
        {
            gate.SetActive(true);
        }

        manager.ChangeState(RoomStateManager.ERoomState.Play);
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Setup");

        manager.Owner.HasBeenSetup = true;
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
    private static void CheckPlayed(RoomStateManager manager)
    {
        switch (manager.Owner.RoomType)
        {
            case Room.ERoomType.Join:
            case Room.ERoomType.Idle:
            case Room.ERoomType.Treasure:
                manager.ChangeState(RoomStateManager.ERoomState.End);
                break;
            case Room.ERoomType.Combat:
                // Wait for all enemies to be dead
                break;
            case Room.ERoomType.Boss:
                // Wait for boss to be dead
                break;
            case Room.ERoomType.End:
                // Wait for player to click on the end level screen
                break;
            default:
                break;
        }
    }

    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter Play");

        manager.Owner.IsPlaying = true;
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit Play");

        manager.Owner.IsPlaying = false;
        manager.Owner.HasBeenPlayed = true;
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update Play");

        CheckPlayed(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// RoomEndState ///////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class RoomEndState : BaseState<RoomStateManager, RoomStateManager.ERoomState, Room>
{
    private void CheckLeft(RoomStateManager manager)
    {
        if (manager.Owner.IsPlayerInside == true)
            return;

        manager.ChangeState(RoomStateManager.ERoomState.Leave);
        return;
    }

    public override void OnEnter(RoomStateManager manager)
    {
        Debug.Log("Enter End");

        foreach (var gate in manager.Owner.Gates)
        {
            gate.SetActive(false);
        }
    }

    public override void OnExit(RoomStateManager manager)
    {
        Debug.Log("Exit End");
    }

    public override void OnUpdate(RoomStateManager manager)
    {
        Debug.Log("Update End");
        CheckLeft(manager);
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

        manager.Owner.NextRoom.IsLocked = false;
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