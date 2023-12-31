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

    public new enum AnimatorCondition
    {
        Talk,
        Run,
        Reload,
        Shoot,
        Dead
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
        _dashInput.action.canceled += OnDashCanceled;

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
        _dashInput.action.canceled -= OnDashCanceled;

        Entity.OnDeath -= OnDeath;

        CancelMove();
        CancelShoot();
        CancelMinimap();
    }

    private void OnDeath()
    {
        SetAnimatorCondition(AnimatorCondition.Dead);

        GameManager.Instance.Stopwatch.StopTime();
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
        SetAnimatorCondition(AnimatorCondition.Run, true);

    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MovementAction.MoveInput = context.ReadValue<Vector2>();
        MovementAction.Execute();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        CancelMove();
    }
    private void CancelMove()
    {
        SetAnimatorCondition(AnimatorCondition.Run, false);

        MovementAction.MoveInput = Vector2.zero;
        MovementAction.Stop();
    }
    #endregion Move

    #region Shoot
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        ShootAction.Execute();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        CancelShoot();
    }
    private void CancelShoot()
    {
        //ShootAction.ResetAnimationSpeed();
        ShootAction.Stop();
    }
    #endregion Shoot

    #region Reload
    private void OnReloadStarted(InputAction.CallbackContext obj)
    {
        SetAnimatorCondition(AnimatorCondition.Reload);
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
        if (DashingAction.Target == null)
            DashingAction.Target = Player.Crosshair.transform;

        DashingAction.Execute();
    }

    private void OnDashCanceled(InputAction.CallbackContext obj)
    {
        DashingAction.Stop();
    }
    #endregion Dash
    #endregion Inputs

    //public void FixedUpdate()
    //{
    //    if (MovementAction.MoveInput != Vector2.zero)
    //    {
    //        MovementAction.FixedMove();
    //    }
    //}

    public void StopReloading()
    {
    }
}