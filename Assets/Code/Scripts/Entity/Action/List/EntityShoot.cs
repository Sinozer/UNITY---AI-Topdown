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
    private float AttackSpeed
    {
        get
        {
            if (NPC)
                return Entity.AttackSpeed;
            else
                return ((Player)Entity).AttackSpeed;
        }
    }

    public float LookX => _direction.x;

    [SerializeField] private SOProjectile _projectileData;

    public float LastTimeShot => _lastTimeShot;
    private float _lastTimeShot;

    public bool IsInShootCooldown => _lastTimeShot + 1 / Entity.AttackSpeed > Time.time;

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
            _target = player.Crosshair.gameObject;
            _targetPosition = _target.transform.position;
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (IsInShootCooldown == true)
                yield return new WaitForSeconds(_lastTimeShot + 1 / AttackSpeed - Time.time);

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

            AudioManager.PlaySFX("Shoot");

            Destroy(projectile, _projectileData.LifeTime);

            // Store the last time the entity shot
            _lastTimeShot = Time.time;

            yield return new WaitForSeconds(1 / AttackSpeed);
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
        Animator.speed = AttackSpeed;
    }

    public void ResetAnimationSpeed()
    {
        Animator.speed = 1f;
    }
}