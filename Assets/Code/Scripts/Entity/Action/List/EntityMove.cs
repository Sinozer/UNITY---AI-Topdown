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

    private float MovementSpeed
    {
        get
        {
            if (NPC)
                return Entity.MovementSpeed;
            else
                return ((Player)Entity).MovementSpeed;
        }
    }

    private void Start()
    {
        Rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (NotNPC)
            return;

        transform.root.GetComponentInChildren<AIPath>().maxSpeed = MovementSpeed / 50f;
    }

    public void FixedMove()
    {
        if (NPC)
            return;

        Rigidbody2D.AddForce(MovementSpeed * 1000 * Time.fixedDeltaTime * MoveInput);
    }

    public void SetAnimationSpeed()
    {
        //// Calculate the speed based on fire rate and adjust the speed of the animator
        Animator.speed = MovementSpeed / 400f;
    }
    public void ResetAnimationSpeed()
    {
        Animator.speed = 1;
    }
}