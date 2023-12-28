// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AnimationEventBridgeEnemy : EntityChild
{
    protected EnemyBrain EnemyBrain => Brain as EnemyBrain;

    public void Die()
    {
        Destroy(transform.root.gameObject);
    }

    public void DieSfx()
    {
        AudioManager.PlaySFX("Death");
    }
}