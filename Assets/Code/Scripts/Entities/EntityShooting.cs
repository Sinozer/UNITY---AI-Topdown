// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;

public class EntityShooting : MonoBehaviour
{
    [SerializeField] private SOProjectile _projectile;
    [SerializeField] private float _fireRate;

    public float LookX => _direction.x;

    private Vector2 _direction;
    private bool _isNpc = true;
    private GameObject _target;
    private Vector2 _targetPosition;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        if (transform.parent.gameObject.CompareTag("Player"))
            _isNpc = false;
    }

    private void Start()
    {
        GetTarget();

        if (_projectile != null)
        {
            // TODO: Create a list of all existing projectile that the GameManager knows

            // TODO: If NULL, get the default projectile from the GameManager list
        }
    }

    private void GetTarget()
    {
        Player player = GameManager.Instance.GetPlayer();

        if (_isNpc == true)
        {
            player = GameManager.Instance.GetPlayer();
            if (player == null)
                return;

            _target = player.gameObject;
            _targetPosition = _target.transform.position;
        }
        else
        {
            player = GameManager.Instance.GetPlayer();
            if (player == null)
                return;

            _target = player.Aim;
            _targetPosition = _target.transform.position;
        }
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            GetTarget();

            GameObject projectile = new GameObject("Projectile");
            projectile.transform.position = transform.position;
            //projectile.transform.rotation = TODO: Look at the target

            projectile.AddComponent<SpriteRenderer>();

            projectile.AddComponent<Animator>().runtimeAnimatorController = _projectile.Controller;
            Rigidbody2D r2d = projectile.AddComponent<Rigidbody2D>();
            r2d.bodyType = RigidbodyType2D.Kinematic;
            _direction = (_targetPosition - (Vector2)transform.position).normalized;

            r2d.velocity = _direction * _projectile.Speed;

            float rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, rotation);

            Destroy(projectile, _projectile.LifeTime);

            yield return new WaitForSeconds(_fireRate);
        }
    }

    public void StartShooting(float fireRate)
    {
        _fireRate = fireRate;
        _shootCoroutine = StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        StopCoroutine(_shootCoroutine);
    }

    public void SetAnimationSpeed(Animator animator)
    {
        // Calculate the speed based on fire rate and adjust the speed of the animator
        animator.speed = 0.5f / 1f;
    }

    public void ResetAnimationSpeed(Animator animator)
    {
        animator.speed = 1f;
    }
}