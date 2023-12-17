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
    [SerializeField] private SOProjectile _projectileData;
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

        if (_projectileData != null)
        {
            // TODO: Create a list of all existing projectile that the GameManager knows

            // TODO: If NULL, get the default projectile from the GameManager list
        }
    }

    private void GetTarget()
    {
        Player player = GameManager.Instance.Player;

        if (_isNpc == true)
        {
            player = GameManager.Instance.Player;
            if (player == null)
                return;

            _target = player.gameObject;
            _targetPosition = _target.transform.position;
        }
        else
        {
            player = GameManager.Instance.Player;
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

            GameObject projectile = Instantiate(GameManager.Instance.Projectile);

            if (_isNpc == true)
                projectile.layer = LayerMask.NameToLayer("OtherProjectile");
            else
                projectile.layer = LayerMask.NameToLayer("PlayerProjectile");

            projectile.transform.position = transform.position;

            _direction = (_targetPosition - (Vector2)transform.position).normalized;
            projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);

            projectile.GetComponent<Animator>().runtimeAnimatorController = _projectileData.Controller;

            projectile.GetComponent<Rigidbody2D>().velocity = _direction * _projectileData.Speed;

            // Add a box collider to the projectile and fix the size automatically
            BoxCollider2D boxCollider = projectile.AddComponent<BoxCollider2D>();
            boxCollider.size = projectile.GetComponent<SpriteRenderer>().size;

            Destroy(projectile, _projectileData.LifeTime);

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