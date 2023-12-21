// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;
public class BossStateManager : BaseStateManager<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    public enum EBossState
    {
        Locked,
        Unlocked,
        Setup,
        Playing,
        Dead,
        Ended
    }

    private static readonly BaseState<BossStateManager, EBossState, BossBrain>[] _states =
    {
        new BossLockedState(),
        new BossUnlockedState(),
        new BossSetupState(),
        new BossPlayingState(),
        new BossDeadState(),
        new BossEndedState()
    };

    public BossStateManager(BossBrain owner) : base(owner)
    {
        _currentState = _states[(int)EBossState.Locked];
        _currentState.OnEnter(this);
    }

    public BossStateManager(BossBrain owner, EBossState state) : base(owner)
    {
        _currentState = _states[(int)state];
        _currentState.OnEnter(this);
    }

    protected override BaseState<BossStateManager, EBossState, BossBrain> GetState(EBossState state)
    {
        return _states[(int)state];
    }
}

////////////////////////////////////////////////////////////////////////////////
/// BossLockedState ////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class BossLockedState : BaseState<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    private void CheckUnlocked(BossStateManager manager)
    {
        if (manager.Owner.IsUnlocked == false)
            return;

        manager.ChangeState(BossStateManager.EBossState.Unlocked);
    }

    public override void OnEnter(BossStateManager manager)
    {
    }

    public override void OnExit(BossStateManager manager)
    {
    }

    public override void OnUpdate(BossStateManager manager)
    {
        CheckUnlocked(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// BossUnlockedState //////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class BossUnlockedState : BaseState<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    private void CheckSetup(BossStateManager manager)
    {
        // For now, we'll just assume that the boss is always ready to be setup

        manager.ChangeState(BossStateManager.EBossState.Setup);
    }

    public override void OnEnter(BossStateManager manager)
    {
    }

    public override void OnExit(BossStateManager manager)
    {
    }

    public override void OnUpdate(BossStateManager manager)
    {
        CheckSetup(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// BossSetupState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class BossSetupState : BaseState<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    private void CheckPlaying(BossStateManager manager)
    {
        // For now, we'll just assume that the boss is always ready to play

        manager.ChangeState(BossStateManager.EBossState.Playing);
    }

    public override void OnEnter(BossStateManager manager)
    {
        manager.Owner.CurrentPhase.IsUnlocked = true;
        manager.Owner.Boss.Rigidbody.simulated = true;
    }

    public override void OnExit(BossStateManager manager)
    {
    }

    public override void OnUpdate(BossStateManager manager)
    {
        CheckPlaying(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// BossPlayingState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class BossPlayingState : BaseState<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    private void CheckDead(BossStateManager manager)
    {
        if (manager.Owner.IsDead == false)
            return;

        manager.Owner.CurrentPhase.IsEnded = true;

        if (manager.Owner.Phase < manager.Owner.Phases.Count - 1)
        {
            manager.Owner.Phase++;
            manager.ChangeState(BossStateManager.EBossState.Setup);
            return;
        }

        manager.ChangeState(BossStateManager.EBossState.Dead);
    }

    public override void OnEnter(BossStateManager manager)
    {
    }

    public override void OnExit(BossStateManager manager)
    {
    }

    public override void OnUpdate(BossStateManager manager)
    {
        CheckDead(manager);

        //if (manager.Owner.CanShootAtPlayer)
        //{
        //    manager.Owner.CurrentPhase.CanAttack = true;
        //    manager.Owner.AIPath(false);
        //    manager.Owner.StartShooting();
        //}

        //if (manager.Owner.SeePlayer)
        //{
        //    manager.Owner.CurrentPhase.SeePlayer = true;
        //    manager.Owner.AIPath(true);

        //}
    }
}

////////////////////////////////////////////////////////////////////////////////
/// BossDeadState //////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class BossDeadState : BaseState<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    private void CheckEnded(BossStateManager manager)
    {
        // For now, we'll just assume that the boss is always ready to end

        manager.ChangeState(BossStateManager.EBossState.Ended);
    }

    public override void OnEnter(BossStateManager manager)
    {
        manager.Owner.AIPath(false);
        manager.Owner.StopShooting();
    }

    public override void OnExit(BossStateManager manager)
    {
    }

    public override void OnUpdate(BossStateManager manager)
    {
        CheckEnded(manager);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// BossEndedState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class BossEndedState : BaseState<BossStateManager, BossStateManager.EBossState, BossBrain>
{
    public override void OnEnter(BossStateManager manager)
    {
        Debug.LogWarning("Boss has ended");
    }

    public override void OnExit(BossStateManager manager)
    {
    }

    public override void OnUpdate(BossStateManager manager)
    {
    }
}