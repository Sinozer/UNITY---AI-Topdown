// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ShootableLinear : MonoBehaviour, IShootable
{
    public void Shoot(Rigidbody2D rigibody, Vector2 direction, float speed)
    {
        rigibody.velocity = direction * speed;
    }
}
