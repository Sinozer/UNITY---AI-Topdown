using UnityEngine;

public class AnimationEventBridge : MonoBehaviour
{
    public PlayerBrain PlayerBrain; // reference to your main script

    // Animation event function
    public void OnReloadAnimationComplete()
    {
        PlayerBrain.StopReloading();
    }

    public void PlayStepSound()
    {
        PlayerBrain.PlayStepSound();
    }
}
