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
    [SerializeField] private Entity _entity;

    [SerializeField] private int _sceneToLoadOnDeath = 0;

    public Player Player => _entity as Player;

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

    
    [Header("References")] 
    [SerializeField] private GameObject _actions;
    [SerializeField] private GameObject _render;
    [SerializeField] private GameObject _minimap;

    private Animator _animator;

    private bool _shoot;

    private EntityMovement _movementAction;
    private EntityShooting _shootingAction;

    private string[] _animatorConditionNames;

    private void Awake()
    {
        _entity = transform.root.GetComponentInChildren<Entity>();
        _movementAction = _actions.GetComponent<EntityMovement>();
        _shootingAction = _actions.GetComponent<EntityShooting>();
        _animator = _render.GetComponent<Animator>();

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

        _entity.OnDeath += OnDeath;
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

        _entity.OnDeath -= OnDeath;
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
    
    void Update()
    {
        if (_entity.IsDead)
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
            _movementAction.Move();
        }

        // Flip the game object based on the direction of the mouse
        var aimPosition = Player.Aim.transform.position;
        var direction = aimPosition - transform.position;

        transform.root.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
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
}