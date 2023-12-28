// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;

public class EntityMove : EntityChild, IEntityAction
{
    public Vector2 MoveInput { get; set; }

    private void Start()
    {
        Rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (NotNPC)
            return;

        transform.root.GetComponentInChildren<AIPath>().maxSpeed = Entity.MovementSpeed / 50f;
    }

    public void FixedMove()
    {
        if (NPC)
            return;

        Rigidbody2D.AddForce(Entity.MovementSpeed * 1000 * Time.fixedDeltaTime * MoveInput);
    }

    public void SetAnimationSpeed()
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        Animator.speed = Entity.MovementSpeed / 400f;
    }
    public void ResetAnimationSpeed()
    {
        Animator.speed = 1;
    }
}