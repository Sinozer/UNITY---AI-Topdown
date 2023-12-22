// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region Fields
    public bool IsNpc => _isNpc;
    [SerializeField] protected bool _isNpc = true;

    public float Health => _health;
    [SerializeField] protected float _health;
    public float MaxHealth => _maxHealth;
    [SerializeField] protected float _maxHealth;

    public float Damage => _damage;
    [SerializeField] protected float _damage;

    public float MovementSpeed => _movementSpeed;
    [SerializeField] protected float _movementSpeed;

    public float AttackSpeed => _attackSpeed;
    [SerializeField] protected float _attackSpeed;
    public float AttackRange => _attackRange;
    [SerializeField] protected float _attackRange;

    public float VisionRange => _visionRange;
    [SerializeField] protected float _visionRange;

    [SerializeField] protected ParticleSystem _vfxHit;
    [SerializeField] protected AudioSource _sfxDying;
    [SerializeField] protected AudioSource _sfxHit;

    public bool IsAlive => _health > 0;
    public bool IsDead => _health <= 0;
    #endregion Fields

    #region Events
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;
    #endregion Events

    public virtual void Heal(float healAmount)
    {
        _health = Mathf.Clamp(_health + healAmount, 0, _maxHealth);

        OnHealthChanged?.Invoke(Health);
    }

    public virtual void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        OnHealthChanged?.Invoke(Health);
        OnHit();

        if (_health <= 0)
            OnDeath?.Invoke();
    }

    public virtual void Die()
    {
        //OnDeath?.Invoke();
        if (IsNpc)
        {
            System.Random random = new System.Random();
            int randomInt = random.Next(0, 100);
            if (randomInt < 10)
                Instantiate(GameManager.Instance.FirstAid, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public virtual void Attack(Entity target)
    {
        target.TakeDamage(_damage);
    }

    public void OnHit()
    {
        // hit sound
        if (_sfxHit != null)
        {
            _sfxHit.Play();
        }

        if (IsAlive)
        {
            // hit particles
            if(_vfxHit != null)
            {
                GameObject go = Instantiate(_vfxHit.gameObject, transform.position, Quaternion.identity, transform);
                go.SetActive(true);
            }
        }
    }

    public void PlayDeathSfx()
    {
        _sfxDying?.Play();
    }
}