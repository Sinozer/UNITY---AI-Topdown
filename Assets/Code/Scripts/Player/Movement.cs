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
    public float Speed = 10f;
    private float _defaultSpeed = 10f;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = ObjectToMove.GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public void Move(float speed)
    {
        Speed = speed;
        rb.MovePosition(rb.position + MoveInput * Speed * Time.fixedDeltaTime);
    }
    
    
    
    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = Speed / _defaultSpeed;
        Debug.Log($"{ Speed / _defaultSpeed}");
    }
    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1;
    }
}