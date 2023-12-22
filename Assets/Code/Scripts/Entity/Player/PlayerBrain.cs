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

public class PlayerBrain : Brain
{
    public Player Player => Entity as Player;

    [SerializeField] private int _sceneToLoadOnDeath = 0;

    private enum AnimatorCondition
    {
        IsIdle,
        IsRun,
        IsTalk,
        IsReload,
        IsShoot,
        IsDead
    }
    
    [Header("Inputs")] 
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private InputActionReference _reloadInput;
    [SerializeField] private InputActionReference _minimapInput;
    [SerializeField] private InputActionReference _dashInput;

    [Header("References")] 
    [SerializeField] private EntityMove _movementAction;
    [SerializeField] private EntityShoot _shootingAction;
    [SerializeField] private EntityDash _dashingAction;
    [SerializeField] private GameObject _minimap;
    [SerializeField] private GameObject _light;
    
    [SerializeField] private AudioSource _sfxStep;

    private Animator _animator;

    private bool _shoot;

    private string[] _animatorConditionNames;

    private void Awake()
    {
        _animator = Render.GetComponent<Animator>();

        _animatorConditionNames = Enum.GetNames(typeof(AnimatorCondition));
    }

    private void OnEnable()
    {
        _moveInput.action.performed += OnMovePerformed;
        _moveInput.action.canceled += OnMoveCanceled;
        _shootInput.action.performed += OnShootPerformed;
        _shootInput.action.canceled += OnShootCanceled;
        _reloadInput.action.performed += OnReloadPerformed;
        _minimapInput.action.performed += OnMinimapPerformed;
        _minimapInput.action.canceled += OnMinimapCanceled;
        _dashInput.action.started += OnDashStarted;
        Entity.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= OnMovePerformed;
        _moveInput.action.canceled -= OnMoveCanceled;
        _shootInput.action.performed -= OnShootPerformed;
        _shootInput.action.canceled -= OnShootCanceled;
        _reloadInput.action.performed -= OnReloadPerformed;
        _minimapInput.action.performed -= OnMinimapPerformed;
        _minimapInput.action.canceled -= OnMinimapCanceled;
        _dashInput.action.started -= OnDashStarted;
        Entity.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        SceneManager.Instance.LoadScene(_sceneToLoadOnDeath);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _movementAction.MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movementAction.MoveInput = Vector2.zero;
        SetAnimatorCondition(AnimatorCondition.IsIdle);
        _movementAction.ResetAnimationSpeed(_animator);
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        _shoot = context.ReadValue<float>() > 0;
        _shootingAction.StartShooting();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        _shoot = false;
        SetAnimatorCondition(AnimatorCondition.IsIdle);
        _shootingAction.ResetAnimationSpeed(_animator);
        _shootingAction.StopShooting();
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        SetAnimatorCondition(AnimatorCondition.IsReload);
    }

    private void OnMinimapPerformed(InputAction.CallbackContext obj)
    {
        _minimap.SetActive(true);
    }

    private void OnMinimapCanceled(InputAction.CallbackContext obj)
    {
        _minimap.SetActive(false);
    }

    private void OnDashStarted(InputAction.CallbackContext obj)
    {
        _dashingAction.TryDash();
    }

    void Update()
    {
        if (Entity.IsDead)
        {
            SetAnimatorCondition(AnimatorCondition.IsDead);
            return;
        }

        if (!_shoot)
        {
            _shootingAction.ResetAnimationSpeed(_animator);
        }
        else
        {
            SetAnimatorCondition(AnimatorCondition.IsShoot);
            _shootingAction.SetAnimationSpeed(_animator);
        }

        if (_movementAction.MoveInput == Vector2.zero)
        {
            _movementAction.ResetAnimationSpeed(_animator);
        }
        else
        {
            SetAnimatorCondition(AnimatorCondition.IsRun);
            _movementAction.SetAnimationSpeed(_animator);
        }

        // Flip the game object based on the direction of the mouse
        var aimPosition = Player.Aim.transform.position;
        var direction = aimPosition - transform.position;

        int degree = direction.x > 0 ? 0 : 180;

        Render.transform.rotation = Quaternion.Euler(0, degree, 0);
        Physics.transform.rotation = Quaternion.Euler(0, degree, 0);

        if (_light != null)
            _light.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void FixedUpdate()
    {
        if (_movementAction.MoveInput != Vector2.zero)
        {
            _movementAction.FixedMove();
        }
    }

    public void StopReloading()
    {
        SetAnimatorCondition(AnimatorCondition.IsIdle);
    }

    private void SetAnimatorCondition(AnimatorCondition trueCondition)
    {
        var trueConditionName = _animatorConditionNames[(int)trueCondition];
        foreach (var conditionName in _animatorConditionNames)
        {
            _animator.SetBool(conditionName, conditionName == trueConditionName);
        }
    }

    public void PlayStepSound()
    {
        if(_sfxStep != null)
            _sfxStep.Play();
    }
}