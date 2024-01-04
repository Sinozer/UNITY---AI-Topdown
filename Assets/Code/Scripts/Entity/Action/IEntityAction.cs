// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for all entity actions
/// </summary>
public abstract class EntityAction : EntityChild
{
    /// <summary>
    /// Reference to the coroutine running the action
    /// </summary>
    protected Coroutine _coroutine;

    /// <summary>
    /// Method called when the action needs to be executed
    /// </summary>
    public void Execute()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Action());
    }

    /// <summary>
    /// Method called when the action needs to be stopped and executed again
    /// </summary>
    /// <remarks> Calls <see cref="Stop"/> and then <see cref="Execute"/> </remarks>
    public void StopAndExecute()
    {
        Stop();
        Execute();
    }

    /// <summary>
    /// Method called by <see cref="Execute"/> to execute the action
    /// </summary>
    /// <remarks> Should only be called by <see cref="Execute"/> </remarks>
    /// <returns> An <see cref="IEnumerator"/> to be used as a coroutine </returns>
    protected abstract IEnumerator Action();

    /// <summary>
    /// Method called when the action needs to be stopped, if applicable
    /// </summary>
    public void Stop()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}

/// <summary>
/// Interface for actions that need a target
/// </summary>
public interface IActionTargetable
{
    /// <summary>
    /// Target of the action.
    /// </summary>
    Transform Target { get; set; }

    float GetAngleToTarget(Vector3 position)
    {
        var direction = Target.position - position;
        return Mathf.Atan2(direction.y, direction.x);
    }

    Vector2 GetNormalizedDirectionToTarget(Vector3 position)
    {
        var angle = GetAngleToTarget(position);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}

/// <summary>
/// Interface for actions that have a cooldown
/// </summary>
public interface IActionCooldown
{
    /// <summary>
    /// Duration of the cooldown in seconds.
    /// </summary>
    float CooldownDuration { get; set; }
    /// <summary>
    /// Time at which the action was last used.
    /// </summary>
    float LastUseTime { get; set; }

    /// <summary>
    /// Tells if the action is on cooldown.
    /// </summary>
    bool IsOnCooldown => LastUseTime + CooldownDuration > Time.time;

    /// <summary>
    /// Starts the cooldown.
    /// </summary>
    void StartCooldown() => LastUseTime = Time.time;

    /// <summary>
    /// Gets the time remaining on the cooldown.
    /// </summary>
    /// <returns> Time remaining on the cooldown in seconds </returns>
    float GetCooldownTimeRemaining() => LastUseTime + CooldownDuration - Time.time;
}