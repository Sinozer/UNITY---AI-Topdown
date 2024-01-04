// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class EntityDash : EntityAction, IActionCooldown, IActionTargetable
{
    [ShowInInspector, ReadOnly]
    public Transform Target { get; set; }

    [ShowInInspector]
    public float Power { get; set; } = 40f;

    [ShowInInspector]
    public float CooldownDuration { get; set; } = 4f;
    public float LastUseTime { get; set; }

    protected override IEnumerator Action()
    {
        if ((this as IActionCooldown).IsOnCooldown)
            yield return new WaitForSeconds((this as IActionCooldown).GetCooldownTimeRemaining());

        (this as IActionCooldown).StartCooldown();

        AudioManager.PlaySFX("Dash");

        // Get the velocity of the dash
        var velocity = (this as IActionTargetable).GetNormalizedDirectionToTarget(transform.position) * Power;

        // Apply the velocity
        Rigidbody2D.velocity += velocity;

        yield return null;
    }
}