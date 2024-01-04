// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// Simple shoot behavior.
/// This will instantiate a projectile and set its velocity towards the <see cref="Target"/> object.
/// </summary>
public class EntityShoot : EntityAction, IActionTargetable, IActionCooldown
{
    public Transform Target
    {
        get
        {
            if (_target != null)
                return _target;

            Player player = GameManager.Instance.Player;
            if (player == null)
                return null;

            if (Entity.IsNpc == true)
            {
                _target = player.transform;
                _targetPosition = Target.position;
            }
            else
            {
                _target = player.Crosshair.transform;
                _targetPosition = Target.position;
            }

            return _target;
        }
        set
        {
            _target = value;
            _targetPosition = Target.position;
        }
    }
    private Transform _target;

    // TODO : Assign this elsewhere
    public GameObject Projectile
    {
        get
        {
            if (_projectile == null)
                GameManager.Instance.Blackboard.TryFind("Projectile", out _projectile);

            if (_projectile == null)
                throw new System.Exception("Projectile not found in the blackboard");

            return _projectile;
        }
    }
    private GameObject _projectile;

    // TODO : Assign this elsewhere
    private float AttackSpeed
    {
        get
        {
            return Entity.Data.GetValue<float>("AttackSpeed");
            //if (NPC)
            //    return Entity.AttackSpeed;
            //else
            //    return ((Player)Entity).AttackSpeed;
        }
    }

    [SerializeField, InlineEditor] private SOProjectile _projectileData;

    public float LastTimeShot => _lastTimeShot;
    private float _lastTimeShot;

    public bool IsInShootCooldown => (this as IActionCooldown).IsOnCooldown;

    public float CooldownDuration { get => 1 / AttackSpeed; set { } }
    public float LastUseTime { get; set; }

    private Vector2 _direction;
    private Vector2 _targetPosition;

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

    protected override IEnumerator Action()
    {
        while (true)
        {
            if ((this as IActionCooldown).IsOnCooldown)
                yield return new WaitForSeconds((this as IActionCooldown).GetCooldownTimeRemaining());

            (this as IActionCooldown).StartCooldown();

            AudioManager.PlaySFX("Shoot");

            if (NotNPC)
                Brain.SetAnimatorCondition(PlayerBrain.AnimatorCondition.Shoot);

            GameObject projectile = Instantiate(
                Projectile,
                transform.position,
                Quaternion.Euler(
                    0,
                    0,
                    (this as IActionTargetable).GetAngleToTarget(transform.position) * Mathf.Rad2Deg
                )
            );

            if (Entity.IsNpc == true)
                projectile.layer = LayerMask.NameToLayer("OtherProjectile");
            else
                projectile.layer = LayerMask.NameToLayer("PlayerProjectile");

            projectile.GetComponent<Animator>().runtimeAnimatorController = _projectileData.Controller;

            projectile.GetComponent<Rigidbody2D>().velocity = (this as IActionTargetable).GetNormalizedDirectionToTarget(transform.position) * _projectileData.Speed;

            BoxCollider2D collider = projectile.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(0.35f, 0.12f);
            collider.offset = new Vector2(0.08f, 0);

            projectile.GetComponent<Projectile>().Damage = _projectileData.Damage;

            Destroy(projectile, _projectileData.LifeTime);

            yield return new WaitForSeconds(CooldownDuration);
        }
    }
}