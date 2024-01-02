// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerBrain : EntityBrain
{
    public Player Player => Entity as Player;

    [SerializeField] private int _sceneToLoadOnDeath = 0;

    public new enum AnimatorCondition
    {
        IsIdle,
        IsRun,
        IsTalk,
        IsReload,
        IsShoot,
        IsDead
    }

    [Header("Inputs")]
    [SerializeField] private InputActionReference _pauseInput;
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private InputActionReference _reloadInput;
    [SerializeField] private InputActionReference _minimapInput;
    [SerializeField] private InputActionReference _dashInput;

    public GameObject Minimap
    {
        get
        {
            if (_minimap == null)
                _minimap = GetExternal<Transform>("Minimap").gameObject;

            return _minimap;
        }
    }
    private GameObject _minimap;
    [SerializeField] private GameObject _light;

    private string[] _animatorConditionNames;

    private void Awake()
    {
        _animatorConditionNames = Enum.GetNames(typeof(AnimatorCondition));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _pauseInput.action.started += OnPauseStarted;

        _moveInput.action.started += OnMoveStarted;
        _moveInput.action.performed += OnMovePerformed;
        _moveInput.action.canceled += OnMoveCanceled;

        _shootInput.action.performed += OnShootPerformed;
        _shootInput.action.canceled += OnShootCanceled;

        _reloadInput.action.started += OnReloadStarted;
        _reloadInput.action.canceled += OnReloadCanceled;

        _minimapInput.action.performed += OnMinimapPerformed;
        _minimapInput.action.canceled += OnMinimapCanceled;

        _dashInput.action.started += OnDashStarted;

        Entity.OnDeath += OnDeath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _pauseInput.action.started -= OnPauseStarted;

        _moveInput.action.started -= OnMoveStarted;
        _moveInput.action.performed -= OnMovePerformed;
        _moveInput.action.canceled -= OnMoveCanceled;

        _shootInput.action.performed -= OnShootPerformed;
        _shootInput.action.canceled -= OnShootCanceled;

        _reloadInput.action.started -= OnReloadStarted;
        _reloadInput.action.canceled -= OnReloadCanceled;

        _minimapInput.action.performed -= OnMinimapPerformed;
        _minimapInput.action.canceled -= OnMinimapCanceled;

        _dashInput.action.started -= OnDashStarted;

        Entity.OnDeath -= OnDeath;

        CancelMove();
        CancelShoot();
        CancelMinimap();
    }

    private void OnDeath()
    {
        Animator.SetTrigger("Dead");

        enabled = false;
        GameManager.Instance.Blackboard.SetValue<Player>("Player", null);

        PlayerManager.Instance.Stopwatch.StopTime();
        MenuManager.Instance.DefaultMenuName = "GameLost";
        MenuManager.Instance.IsMenuOpen = true;
    }

    #region Inputs
    #region Pause
    private void OnPauseStarted(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.Game)
        {
            GameManager.Instance.Pause();
        }
        else if (GameManager.Instance.CurrentGameState == GameManager.GameState.Pause)
        {
            GameManager.Instance.Resume();
        }
    }
    #endregion Pause

    #region Move
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        Animator.SetBool("Run", true);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MovementAction.MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        CancelMove();
    }
    private void CancelMove()
    {
        Animator.SetBool("Run", false);

        MovementAction.MoveInput = Vector2.zero;
        MovementAction.ResetAnimationSpeed();
    }
    #endregion Move

    #region Shoot
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        ShootAction.StartShooting();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        CancelShoot();
    }
    private void CancelShoot()
    {
        //ShootAction.ResetAnimationSpeed();
        ShootAction.StopShooting();
    }
    #endregion Shoot

    #region Reload
    private void OnReloadStarted(InputAction.CallbackContext obj)
    {
        Animator.SetTrigger("Reload");
    }

    private void OnReloadCanceled(InputAction.CallbackContext obj)
    {
        // NOT USED
        return;
    }
    #endregion Reload

    #region Minimap
    private void OnMinimapPerformed(InputAction.CallbackContext obj)
    {
        Minimap.SetActive(true);
    }

    private void OnMinimapCanceled(InputAction.CallbackContext obj)
    {
        CancelMinimap();
    }
    private void CancelMinimap()
    {
        Minimap.SetActive(false);
    }
    #endregion Minimap

    #region Dash
    private void OnDashStarted(InputAction.CallbackContext obj)
    {
        DashingAction.TryDash();
    }
    #endregion Dash
    #endregion Inputs

    public void FixedUpdate()
    {
        if (MovementAction.MoveInput != Vector2.zero)
        {
            MovementAction.FixedMove();
        }
    }

    public void StopReloading()
    {
    }
}