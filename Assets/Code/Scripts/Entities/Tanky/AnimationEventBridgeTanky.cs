using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventBridgeTanky : MonoBehaviour
{
    public TankyBrain TankyBrain; // reference to your main script

    // Animation event function
    public void OnReloadAnimationComplete()
    {
        TankyBrain.EndActivating();
    }
    public void Die()
    { 
        TankyBrain.Die();
    }
}
