using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventBridge : MonoBehaviour
{
    public PlayerBrain PlayerBrain; // reference to your main script

    public void OnDeathComplete()
    {
        Destroy(transform.root.gameObject);

        CinemachineTargetGroup vcam = FindFirstObjectByType<CinemachineTargetGroup>();
        if (vcam == null)
            return;

        // Remove all the vcam targets
        vcam.m_Targets = new CinemachineTargetGroup.Target[0];
    }

    public void OnReloadAnimationComplete()
    {
        PlayerBrain.StopReloading();
    }

    public void PlayStepSound()
    {
        PlayerBrain.AudioManager.PlaySFX("Step");
    }
}
