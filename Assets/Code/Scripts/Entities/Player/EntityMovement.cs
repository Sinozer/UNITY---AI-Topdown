// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Entity _entity;

    public Vector2 MoveInput { get; set; }
    private Rigidbody2D _rb;

    private void Awake()
    {
        if (_entity == null)
            _entity = transform.root.GetComponentInChildren<Entity>();
    }

    private void Start()
    {
        _rb = transform.root.GetComponentInChildren<Rigidbody2D>();
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (_entity.IsNpc == false)
            return;

        transform.root.GetComponentInChildren<AIPath>().maxSpeed = _entity.MovementSpeed / 50f;
    }

    public void FixedMove()
    {
        //_rb.MovePosition(_rb.position + MoveInput * _entity.MovementSpeed * Time.fixedDeltaTime);
        _rb.AddForce(_entity.MovementSpeed * 1000 * Time.fixedDeltaTime * MoveInput);
    }

    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = 1;
    }
    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1;
    }
}