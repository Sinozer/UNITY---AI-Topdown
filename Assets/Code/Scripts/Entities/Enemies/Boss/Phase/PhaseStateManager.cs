// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class PhaseStateManager : BaseStateManager<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    public enum EPhaseState
    {
        Locked,         // The phase is locked
        Unlocked,       // The phase is unlocked
        Setup,          // The phase is being setup
        Playing,        // The phase is playing
        Ended           // The phase has ended
    }

    private static readonly BaseState<PhaseStateManager, EPhaseState, Phase>[] _states =
    {
        new PhaseLockedState(),
        new PhaseUnlockedState(),
        new PhaseSetupState(),
        new PhasePlayingState(),
        new PhaseEndedState()
    };

    public PhaseStateManager(Phase owner) : base(owner)
    {
        _currentState = _states[(int)EPhaseState.Locked];
        _currentState.OnEnter(this);
    }

    public PhaseStateManager(Phase owner, EPhaseState state) : base(owner)
    {
        _currentState = _states[(int)state];
        _currentState.OnEnter(this);
    }

    protected override BaseState<PhaseStateManager, EPhaseState, Phase> GetState(EPhaseState state)
    {
        return _states[(int)state];
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseLockedState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseLockedState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckUnlocked(PhaseStateManager manager)
    {
        if (manager.Owner.IsUnlocked == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Unlocked);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckUnlocked(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseUnlockedState /////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseUnlockedState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckSetup(PhaseStateManager manager)
    {
        // For now, we'll just assume that the phase is always ready to be setup

        manager.ChangeState(PhaseStateManager.EPhaseState.Setup);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        manager.Owner.InvokeOnPhaseUnlockedStarting();
    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckSetup(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseSetupState ////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseSetupState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckPlaying(PhaseStateManager manager)
    {
        if (manager.Owner.HasBeenSetup == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Playing);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        manager.Owner.InvokeOnPhaseSetupStarting();
    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckPlaying(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhasePlayingState //////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhasePlayingState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckEnded(PhaseStateManager manager)
    {
        if (manager.Owner.IsEnded == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Ended);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        manager.Owner.IsPlaying = true;
        manager.Owner.InvokeOnPhasePlayingStarting();
    }

    public override void OnExit(PhaseStateManager manager)
    {
        manager.Owner.IsPlaying = false;
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckEnded(manager);

        // Phase logic here
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseEndedState ////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseEndedState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    public override void OnEnter(PhaseStateManager manager)
    {
        manager.Owner.InvokeOnPhaseEndedStarting();
    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
    }
}