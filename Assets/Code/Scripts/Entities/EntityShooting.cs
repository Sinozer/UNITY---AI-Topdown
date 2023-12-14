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
    private bool _isNpc = true;

    private GameObject _target;
    private Vector2 _targetPosition;

    private Coroutine _shootCoroutine;

    [SerializeField] private SOProjectile _projectile;

    public float LookX => _direction.x;
    private Vector2 _direction;

    private void Awake()
    {
        if (CompareTag("Player"))
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
            if (_target == null)
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

            Destroy(projectile, _projectile.LifeTime);

            yield return new WaitForSeconds(1);
        }
    }

    public void StartShooting()
    {
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