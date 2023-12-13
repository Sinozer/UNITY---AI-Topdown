// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Player;
    
    public Vector2 MoveInput { get; set; }
    
    [Header("Sats")]
    public float Speed = 4f;
    private float _defaultSpeed = 4f;

    public void Move()
    {
        Player.Translate(MoveInput * (Time.deltaTime * Speed));
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