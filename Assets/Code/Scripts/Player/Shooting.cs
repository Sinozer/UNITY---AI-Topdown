
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    
    
    // The time between each shot
    public float FireRate = 0.5f;
    // When the next shot will be available
    private float _nextFireTime = 0f;
    private float _defaultFireRate = 0.5f;
    
    public void Shoot()
    {

        if (Time.time > _nextFireTime)
        {
            Vector2 playerPosition = transform.position;
            Vector2 mousePosition = _aim.position;

            Vector2 direction = (mousePosition - playerPosition).normalized;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Create a bullet
            GameObject bullet = Instantiate(bulletPrefab,
                playerPosition,
                Quaternion.Euler(0, 0, rotation));

            // Add force to the bullet in the direction of the mouse
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
            
            _nextFireTime = Time.time + FireRate;
        }
    }
    
    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = _defaultFireRate / FireRate;
    }
    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1;
    }
}
