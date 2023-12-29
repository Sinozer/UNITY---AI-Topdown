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

    public PlayerManager PlayerManager
    {
        get
        {
            if (_playerManager == null)
                _playerManager = PlayerManager.Instance;

            return _playerManager;
        }
    }
    private PlayerManager _playerManager;

    #region Data
    public new float Health
    {
        get => PlayerManager.Health.Acquire();
        set => PlayerManager.Health.Set(value);
    }
    public new float MaxHealth
    {
        get => PlayerManager.Data.Acquire().MaxHealth;
        set => PlayerManager.Data.Acquire().MaxHealth = value;
    }
    public new float Damage
    {
        get => PlayerManager.Data.Acquire().Damage;
        set => PlayerManager.Data.Acquire().Damage = value;
    }
    public new float MovementSpeed
    {
        get => PlayerManager.Data.Acquire().MovementSpeed;
        set => PlayerManager.Data.Acquire().MovementSpeed = value;
    }
    public new float AttackSpeed
    {
        get => PlayerManager.Data.Acquire().AttackSpeed;
        set => PlayerManager.Data.Acquire().AttackSpeed = value;
    }
    public new float AttackRange
    {
        get => PlayerManager.Data.Acquire().AttackRange;
        set => PlayerManager.Data.Acquire().AttackRange = value;
    }
    public new float VisionRange
    {
        get => PlayerManager.Data.Acquire().VisionRange;
        set => PlayerManager.Data.Acquire().VisionRange = value;
    }
    #endregion Data

    protected override void Awake()
    {
        SOEntity data = PlayerManager.Data.Acquire();

        if (data != null)
            _baseData = data;

        base.Awake();

        _isNpc = false;

        PlayerManager.Data.Set(Instantiate(_baseData));
    }

    private void Start()
    {
        GameManager.Instance.Player = this;

        CinemachineTargetGroup vcam = FindFirstObjectByType<CinemachineTargetGroup>();
        if (vcam == null)
            return;

        _crosshair = Instantiate(_crosshair);
        vcam.AddMember(transform, 3, 1);
    }
}