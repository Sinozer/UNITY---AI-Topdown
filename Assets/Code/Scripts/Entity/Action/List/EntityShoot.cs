// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using System.Linq;
using UnityEngine;

public class EntityShoot : EntityChild, IEntityAction
{
    public float LookX => _direction.x;

    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private SOProjectile _projectileData;

    private Vector2 _direction;
    private GameObject _target;
    private Vector2 _targetPosition;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        if (_projectileData == null)
        {
            var list = GameManager.Instance.ProjectileList.List;
            // TODO : NOT TESTED
            if (list.ContainsKey(name))
                _projectileData = list[name];
            else
                _projectileData = list.First().Value;
        }
    }

    private void GetTarget()
    {
        Player player = GameManager.Instance.Player;
        if (player == null)
            return;

        if (Entity.IsNpc == true)
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

            if (Entity.IsNpc == true)
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

            if (_shootSound != null)
                _shootSound.Play();

            Destroy(projectile, _projectileData.LifeTime);

            yield return new WaitForSeconds(1 / Entity.AttackSpeed);
        }
    }

    public void StartShooting()
    {
        if (_shootCoroutine != null)
            return;

        _shootCoroutine = StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        if (_shootCoroutine == null)
            return;

        StopCoroutine(_shootCoroutine);
        _shootCoroutine = null;
    }

    public void SetAnimationSpeed()
    {
        // TODO: Fix this -> Animation and projectile are not sync
        Animator.speed = Entity.AttackSpeed;
    }

    public void ResetAnimationSpeed()
    {
        Animator.speed = 1f;
    }
}