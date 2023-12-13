// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
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
    [SerializeField] private InputActionReference  _moveInput;
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private InputActionReference _reloadInput;
    
    
    [Header("References")]
    [SerializeField] private GameObject Actions;
    [SerializeField] private GameObject Render;

    private Animator _animator;
    private PlayerInput _playerInput;

    private bool _shoot;

    private Movement _movementAction;
    private Shooting _shootingAction;
    
    private string[] animatorConditionNames;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementAction = Actions.GetComponent<Movement>();
        _shootingAction = Actions.GetComponent<Shooting>();
        _animator = Render.GetComponent<Animator>();

        animatorConditionNames = Enum.GetNames(typeof(AnimatorCondition));
    }

    private void OnEnable()
    {
        _moveInput.action.performed += OnMovePerformed;
        _moveInput.action.canceled += OnMoveCanceled;
        _shootInput.action.performed += OnShootPerformed;
        _shootInput.action.canceled += OnShootCanceled;
        _reloadInput.action.performed += OnReloadPerformed;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= OnMovePerformed;
        _moveInput.action.canceled -= OnMoveCanceled;
        _shootInput.action.performed -= OnShootPerformed;
        _shootInput.action.canceled -= OnShootCanceled;
        _reloadInput.action.performed -= OnReloadPerformed;
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
        _shoot = context.ReadValue<float>() > 0f;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        _shoot = false;
        SetAnimatorCondition(AnimatorCondition.IsIdle);
        _shootingAction.ResetAnimationSpeed(_animator);
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        SetAnimatorCondition(AnimatorCondition.IsReload);
    }
    
    void Update()
    {
        if (_shoot)
        {
            SetAnimatorCondition(AnimatorCondition.IsShoot);
            _shootingAction.SetAnimationSpeed(_animator);
            _shootingAction.Shoot();
        }
        else
        {
            _shootingAction.ResetAnimationSpeed(_animator);
        }

        if (_movementAction.MoveInput != Vector2.zero)
        {
            SetAnimatorCondition(AnimatorCondition.IsRun);
            _movementAction.SetAnimationSpeed(_animator);
            _movementAction.Move();
        }
        else
        {
            _movementAction.ResetAnimationSpeed(_animator);
        }
        
        Render.GetComponent<SpriteRenderer>().flipX = !(_shootingAction.LookX > 0);
    }

    public void StopReloading()
    {
        SetAnimatorCondition(AnimatorCondition.IsIdle);
    }

    private void SetAnimatorCondition(AnimatorCondition trueCondition)
    {
        var trueConditionName = animatorConditionNames[(int)trueCondition];
        foreach (var conditionName in animatorConditionNames)
        {
            _animator.SetBool(conditionName, conditionName == trueConditionName);
        }
    }
}