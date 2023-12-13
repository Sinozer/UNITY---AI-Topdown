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

    private bool aaa;

    private Movement _movement;
    private Shooting _shooting;
    
    private string[] animatorConditionNames;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movement = Actions.GetComponent<Movement>();
        _shooting = Actions.GetComponent<Shooting>();
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
        _movement.MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movement.MoveInput = Vector2.zero;
        SetAnimatorCondition(AnimatorCondition.IsIdle);
        _movement.ResetAnimationSpeed(_animator);
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        aaa = context.ReadValue<float>() > 0f;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        aaa = false;
        SetAnimatorCondition(AnimatorCondition.IsIdle);
        _shooting.ResetAnimationSpeed(_animator);
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        SetAnimatorCondition(AnimatorCondition.IsReload);
    }
    
    void Update()
    {
        if (aaa)
        {
            SetAnimatorCondition(AnimatorCondition.IsShoot);
            _shooting.SetAnimationSpeed(_animator);
            _shooting.Shoot();
        }
        else
        {
            _shooting.ResetAnimationSpeed(_animator);
        }

        if (_movement.MoveInput != Vector2.zero)
        {
            SetAnimatorCondition(AnimatorCondition.IsRun);
            _movement.SetAnimationSpeed(_animator);
            _movement.Move();
        }
        else
        {
            _movement.ResetAnimationSpeed(_animator);
        }
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