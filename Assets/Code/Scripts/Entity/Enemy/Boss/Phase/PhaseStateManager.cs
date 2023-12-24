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
        Idle,           // The phase is idle
        Patrol,         // The phase is patrolling, moving from one point to another            | NOT USED FOR NOW
        Move,           // The phase is moving, moving to a specific point (Player, etc.)
        Attack,         // The phase is attacking, attacking a specific target (Player, etc.)
        Ended           // The phase has ended
    }

    private static readonly BaseState<PhaseStateManager, EPhaseState, Phase>[] _states =
    {
        new PhaseLockedState(),
        new PhaseIdleState(),
        new PhasePatrolState(),
        new PhaseMoveState(),
        new PhaseAttackState(),
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

        manager.ChangeState(PhaseStateManager.EPhaseState.Idle);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        Debug.Log("Phase is locked");

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
/// PhaseIdleState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseIdleState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckEnded(PhaseStateManager manager)
    {
        if (manager.Owner.IsEnded == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Ended);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        Debug.Log("Phase is idle");

    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckEnded(manager);

        if (manager.Owner.BossBrain.IsInShootRange)
        {
            manager.ChangeState(PhaseStateManager.EPhaseState.Attack);
            return;
        }

        if (manager.Owner.BossBrain.IsInVisionRange)
        {
            manager.ChangeState(PhaseStateManager.EPhaseState.Move);
            return;
        }


    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhasePatrolState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhasePatrolState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckEnded(PhaseStateManager manager)
    {
        if (manager.Owner.IsEnded == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Ended);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        Debug.Log("Phase is patrolling");

    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckEnded(manager);

        if (manager.Owner.BossBrain.IsInShootRange)
        {
            manager.ChangeState(PhaseStateManager.EPhaseState.Attack);
            return;
        }

        if (manager.Owner.BossBrain.IsInVisionRange)
        {
            manager.ChangeState(PhaseStateManager.EPhaseState.Move);
            return;
        }

        manager.ChangeState(PhaseStateManager.EPhaseState.Idle);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseMoveState /////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseMoveState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckEnded(PhaseStateManager manager)
    {
        if (manager.Owner.IsEnded == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Ended);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        Debug.Log("Phase is moving");

        manager.Owner.BossBrain.AIPath(true);
        manager.Owner.BossBrain.FollowingPlayer(true);
        // Do things for the animation
        manager.Owner.BossBrain.Animator.SetBool("Run", true);
    }

    public override void OnExit(PhaseStateManager manager)
    {
        manager.Owner.BossBrain.AIPath(false);
        manager.Owner.BossBrain.FollowingPlayer(false);
        // Do things for the animation
        manager.Owner.BossBrain.Animator.SetBool("Run", false);
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckEnded(manager);

        switch (((SOPhase)manager.Owner.BaseData).Enraged)
        {
            case true:
                break;
            case false:
                break;
        }

        if (manager.Owner.BossBrain.IsInShootRange)
        {
            manager.ChangeState(PhaseStateManager.EPhaseState.Attack);
            return;
        }

        if (manager.Owner.BossBrain.IsInVisionRange)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Idle);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseAttackState ///////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseAttackState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    private void CheckEnded(PhaseStateManager manager)
    {
        if (manager.Owner.IsEnded == false)
            return;

        manager.ChangeState(PhaseStateManager.EPhaseState.Ended);
    }

    public override void OnEnter(PhaseStateManager manager)
    {
        Debug.Log("Phase is attacking");

        manager.Owner.BossBrain.StartShooting();
        // Do things for the animation
        manager.Owner.BossBrain.Animator.SetBool("Attack_Gun", true);
    }

    public override void OnExit(PhaseStateManager manager)
    {
        manager.Owner.BossBrain.StopShooting();
        // Do things for the animation
        manager.Owner.BossBrain.Animator.SetBool("Attack_Gun", false);
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
        CheckEnded(manager);

        switch (((SOPhase)manager.Owner.BaseData).Enraged)
        {
            case true:

                // Can shoot rockets

                // Can spawn Akbar

                // Can spawn firezones

                break;
            case false:

                // Can shoot rockets

                // Can spawn Akbar

                break;
        }

        if (manager.Owner.BossBrain.IsInShootRange)
            return;

        if (manager.Owner.BossBrain.IsInVisionRange)
        {
            manager.ChangeState(PhaseStateManager.EPhaseState.Move);
            return;
        }

        manager.ChangeState(PhaseStateManager.EPhaseState.Idle);
    }
}

////////////////////////////////////////////////////////////////////////////////
/// PhaseEndedState ////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

public class PhaseEndedState : BaseState<PhaseStateManager, PhaseStateManager.EPhaseState, Phase>
{
    public override void OnEnter(PhaseStateManager manager)
    {
        Debug.Log("Phase is ended");

    }

    public override void OnExit(PhaseStateManager manager)
    {
    }

    public override void OnUpdate(PhaseStateManager manager)
    {
    }
}