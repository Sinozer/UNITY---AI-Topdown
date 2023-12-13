// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    public Transform ObjectToMove;
    
    public Vector2 MoveInput { get; set; }
    
    [Header("Sats")]
    public float Speed = 1f;
    private float _defaultSpeed = 1f;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = ObjectToMove.GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public void Move(float speed)
    {
        Speed = speed;
        rb.AddForce(MoveInput * Speed);
    }
    
    
    
    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = Speed / _defaultSpeed;
    }
    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1;
    }
}