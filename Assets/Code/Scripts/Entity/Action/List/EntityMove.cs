// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System.Collections;
using UnityEngine;

public class EntityMove : EntityAction
{
    public Vector2 MoveInput { get; set; }

    private float MovementSpeed
    {
        get
        {
            return Entity.Data.GetValue<float>("MovementSpeed");
            //if (NPC)
            //    return Entity.Data.GetValue<float>("MovementSpeed");
            //else
            //    return ((Player)Entity).MovementSpeed;
        }
    }

    private void Start()
    {
        Rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (NotNPC)
            return;

        transform.root.GetComponentInChildren<AIPath>().maxSpeed = MovementSpeed / 50f;
    }

    protected override IEnumerator Action()
    {
        while (true)
        {
            if (NPC)
                yield break;

            Rigidbody2D.AddForce(MovementSpeed * 1000 * Time.fixedDeltaTime * MoveInput);

            yield return new WaitForFixedUpdate();
        }
    }
}