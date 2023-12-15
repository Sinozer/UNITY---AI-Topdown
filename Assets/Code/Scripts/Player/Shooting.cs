using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _aim;

    public GameObject BulletPrefab;
    public float BulletSpeed = 10f;


    // The time between each shot
    public float FireRate = 0.5f;

    private readonly float _defaultFireRate = 0.5f;

    public float LookX => _direction.x;

    private Vector2 _direction;
    private Vector2 _playerPosition;
    private Vector2 _mousePosition;
    
    private Coroutine _shootCoroutine;

    private void Update()
    {
        _playerPosition = transform.position;
        _mousePosition = _aim.position;

        _direction = (_mousePosition - _playerPosition).normalized;
    }

    public void SetFireRate(float fireRate)
    {
        FireRate = fireRate;
    }

    public void StartShooting()
    {
        _shootCoroutine = StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        StopCoroutine(_shootCoroutine);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            // Create a bullet
            GameObject bullet = Instantiate(BulletPrefab,
                _playerPosition,
                Quaternion.Euler(0, 0, rotation));

            // Add force to the bullet in the direction of the mouse
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = _direction * BulletSpeed;

            yield return new WaitForSecondsRealtime(FireRate);
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