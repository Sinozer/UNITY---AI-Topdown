// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerBrain : MonoBehaviour
{
    
   
    

    [Header("Ref")]
    public GameObject Actions;
    
    
    
    private PlayerInput _playerInput;
    
    private bool _shootInput;

    // Refer to other scripts
    private Movement _movement;
    private Shooting _shooting;

    

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movement = Actions.GetComponent<Movement>();
        _shooting = Actions.GetComponent<Shooting>();
    }
    

    private void OnEnable()
    {
        // Subscribe to events
        _playerInput.actions["Move"].performed += OnMovePerformed;
        _playerInput.actions["Move"].canceled += OnMoveCanceled;
        _playerInput.actions["Shoot"].performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        _playerInput.actions["Move"].performed -= OnMovePerformed;
        _playerInput.actions["Move"].canceled -= OnMoveCanceled;
        _playerInput.actions["Shoot"].performed -= OnShootPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _movement.MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _movement.MoveInput = Vector2.zero;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        _shootInput = context.ReadValue<float>() > 0f;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (_shootInput) _shooting.Shoot();
        if (_movement.MoveInput != Vector2.zero) _movement.Move();
    }
}