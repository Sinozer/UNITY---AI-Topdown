// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AnimationEventBridgeTanky : AnimationEventBridgeEnemy
{
    private TankyBrain TankyBrain => EnemyBrain as TankyBrain;

    // Animation event function
    public void OnReloadAnimationComplete()
    {
        TankyBrain.EndActivating();
    }
}
