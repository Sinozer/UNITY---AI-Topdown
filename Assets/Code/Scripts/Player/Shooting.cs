using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _aim;

    public Weapon weapon;

    
    

    private readonly float _defaultFireRate = 0.5f;

    public float LookX => _direction.x;

    private Vector2 _direction;
    private Vector2 _playerPosition;
    private Vector2 _mousePosition;
    
    private Coroutine _shootCoroutine;

    private void Update()
    {
        _playerPosition  = transform.position;
        _mousePosition = _aim.position;
        _direction = (_mousePosition - _playerPosition).normalized;
    }

    public void StartShooting()
    {
        _shootCoroutine = StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        StopCoroutine(_shootCoroutine);
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            weapon.Shoot();

            yield return new WaitForSeconds(1 / weapon.FireRate);
        }
    }

    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = _defaultFireRate / weapon.FireRate;
    }

    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1;
    }
}