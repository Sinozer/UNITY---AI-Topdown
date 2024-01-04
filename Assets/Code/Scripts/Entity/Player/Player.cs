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
    [SerializeField] private Crosshair _crosshair;
    public Crosshair Crosshair => _crosshair;

    protected override void Awake()
    {
        //SOEntity data = PlayerManager.Data.Acquire();

        //if (data != null)
        //    _baseData = data;

        base.Awake();

        _isNpc = false;

        //PlayerManager.Data.Set(Instantiate(_baseData));
    }

    private void Start()
    {
        GameManager.Instance.Blackboard.SetValue<Player>("Player", this);

        CinemachineTargetGroup vcam = FindFirstObjectByType<CinemachineTargetGroup>();
        if (vcam == null)
            return;

        _crosshair = Instantiate(_crosshair);
        vcam.AddMember(transform, 3, 1);
    }

    private void OnDestroy()
    {
        if (GameManager.IsInitialized == false)
            return;

        if (GameManager.Instance.Blackboard.TryFind<Player>("Player", out Player player))
        {
            if (player == this)
                GameManager.Instance.Blackboard.SetValue<Player>("Player", null);
        }
    }
}