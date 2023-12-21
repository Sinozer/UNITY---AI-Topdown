// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using System.Linq;
using UnityEngine;

public class EntityShooting : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private AudioSource _shootSound;

    private void Awake()
    {
        _entity = GetComponentInParent<Entity>();

        if (_projectileData == null)
        {
            var list = GameManager.Instance.ProjectileList.List;
            // NOT TESTED
            if (list.ContainsKey(name))
                _projectileData = list[name];
            else
                _projectileData = list.First().Value;
        }
    }

    [SerializeField] private SOProjectile _projectileData;
    [SerializeField] private float _fireRate;

    public float LookX => _direction.x;

    private Vector2 _direction;
    private GameObject _target;
    private Vector2 _targetPosition;
    private Coroutine _shootCoroutine;

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
        if (player == null)
            return;

        if (_entity.IsNpc == true)
        {

            _target = player.gameObject;
            _targetPosition = _target.transform.position;
        }
        else
        {
            _target = player.Aim;
            _targetPosition = _target.transform.position;
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            GetTarget();

            GameObject projectile = Instantiate(GameManager.Instance.Projectile);

            if (_entity.IsNpc == true)
                projectile.layer = LayerMask.NameToLayer("OtherProjectile");
            else
                projectile.layer = LayerMask.NameToLayer("PlayerProjectile");

            projectile.transform.position = transform.position;

            _direction = (_targetPosition - (Vector2)transform.position).normalized;
            projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);

            projectile.GetComponent<Animator>().runtimeAnimatorController = _projectileData.Controller;

            projectile.GetComponent<Rigidbody2D>().velocity = _direction * _projectileData.Speed;

            //projectile.GetComponent<BoxCollider2D>().size = projectile.GetComponent<SpriteRenderer>().size;
            
            BoxCollider2D collider = projectile.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(0.35f, 0.12f);
            collider.offset = new Vector2(0.08f, 0);

            projectile.GetComponent<Projectile>().Damage = _projectileData.Damage;

            _shootSound?.Play();

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