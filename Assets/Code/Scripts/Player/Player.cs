// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _aim;
    public GameObject Aim => _aim;

    private void Start()
    {
        GameManager.Instance.Player = this;

        // Instantiate new empty gameobject
        _aim = new GameObject("Aim");
        _aim.AddComponent<FollowCursor>();

        CinemachineTargetGroup vcam = FindFirstObjectByType<CinemachineTargetGroup>();
        if (vcam == null)
            return;

        vcam.AddMember(_aim.transform, 1, 1);
        vcam.AddMember(transform, 3, 1);
    }
}