// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EntityDamageable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Projectile>(out var projectile))
            return;

        if (!transform.parent.TryGetComponent<Entity>(out var entity))
            return;

        entity.TakeDamage(projectile.Damage);
    }
}