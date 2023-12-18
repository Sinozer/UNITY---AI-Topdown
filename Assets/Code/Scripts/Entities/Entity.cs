// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
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

    public virtual void Heal(float healAmount)
    {
        _health = Mathf.Clamp(_health + healAmount, 0, _maxHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;

        Debug.Log($"{name} took {damage} damage. Health: {_health}");

        if (_health <= 0)
        {
            //Die();
        }
    }

    public virtual void Die(float timeBeforeDestroy = 0)
    {
        Destroy(transform.parent.gameObject, timeBeforeDestroy);
    }

    public virtual void Attack(Entity target)
    {
        target.TakeDamage(_damage);
    }
}