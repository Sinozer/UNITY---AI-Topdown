// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Cinemachine;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private GameObject _aim;
    public GameObject Aim => _aim;

    private void Awake()
    {
        _isNpc = false;
    }

    private void Start()
    {
        GameManager.Instance.Player = this;

        CinemachineTargetGroup vcam = FindFirstObjectByType<CinemachineTargetGroup>();
        if (vcam == null)
            return;

        // Instantiate new empty gameobject
        _aim = new GameObject("Aim");
        _aim.AddComponent<FollowCursor>();

        vcam.AddMember(_aim.transform, 1, 1);
        vcam.AddMember(transform, 3, 1);
    }
}