// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EntityDamageControl : EntityChild
{
    [SerializeField] private float _multiplier = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Projectile>(out var projectile))
            return;

        if (!transform.root.TryGetComponent<Entity>(out var entity))
            return;

        if (projectile.AlreadyHit)
            return;

        projectile.AlreadyHit = true;
        entity.TakeDamage(projectile.Damage * _multiplier);
    }
}