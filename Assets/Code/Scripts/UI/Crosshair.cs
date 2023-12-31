// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Cinemachine;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
#endif
        Player player = GameManager.Instance.Player;
        if (player == null)
            return;

        CinemachineTargetGroup vcam = FindFirstObjectByType<CinemachineTargetGroup>();
        if (vcam == null)
            return;

        vcam.AddMember(transform, 1, 1);

        EntityLookAt lookAtAction = player.GetAction<EntityLookAt>();

        if (lookAtAction == false)
            throw new System.Exception("Player does not have a look at action");

        lookAtAction.Target = transform;
    }
}