// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AnimationEventBridgeBoss : AnimationEventBridgeEnemy
{
    private BossBrain _bossBrain => EnemyBrain as BossBrain;

    // Animation event function
    public void OnShootAnimationStart()
    {
        _bossBrain.ShowLegs(true);
    }

    public void OnShootAnimationComplete()
    {
        if (_bossBrain.Animator.GetBool("Attack_Gun") == true)
            return;

        _bossBrain.ShowLegs(false);
    }
}
