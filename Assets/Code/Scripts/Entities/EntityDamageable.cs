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
        Debug.LogWarning("Collision");

        if (!collision.gameObject.TryGetComponent<Projectile>(out var projectile))
            return;

        Debug.LogWarning("Projectile");

        if (!transform.parent.TryGetComponent<Entity>(out var entity))
            return;

        Debug.LogWarning("Entity");

        entity.TakeDamage(projectile.Damage);
    }
}