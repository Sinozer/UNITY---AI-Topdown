// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AnimationEventBridgeTanky : AnimationEventBridgeEnemy
{
    private TankyBrain _tankyBrain => EnemyBrain as TankyBrain;

    // Animation event function
    public void OnReloadAnimationComplete()
    {
        _tankyBrain.EndActivating();
    }
}
