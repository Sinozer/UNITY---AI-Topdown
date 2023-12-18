// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

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
    
    public bool IsAlive => _health > 0;
    public bool IsDead => _health <= 0;
    #endregion Fields

    #region Events
    public delegate void EntityEventHandler(float health);
    public event EntityEventHandler OnHealthChanged;
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
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Attack(Entity target)
    {
        target.TakeDamage(_damage);
    }
}