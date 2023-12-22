// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;

public class EntityMove : EntityAction
{
    public Vector2 MoveInput { get; set; }

    private void Start()
    {
        Rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (NotNPC)
            return;

        transform.root.GetComponentInChildren<AIPath>().maxSpeed = Entity.MovementSpeed / 50f;
    }

    public void FixedMove()
    {
        if (NPC)
            return;

        Rigidbody.AddForce(Entity.MovementSpeed * 1000 * Time.fixedDeltaTime * MoveInput);
    }

    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = Entity.MovementSpeed / 500f;
    }
    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1;
    }
}