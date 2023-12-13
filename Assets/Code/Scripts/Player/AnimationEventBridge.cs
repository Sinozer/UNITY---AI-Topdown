using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventBridge : MonoBehaviour
{
    public PlayerBrain playerBrain; // reference to your main script

    // Animation event function
    public void OnReloadAnimationComplete()
    {
        playerBrain.StopReloading();
    }
}
