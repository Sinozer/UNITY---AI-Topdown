// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Linq;
using UnityEngine;

public abstract class Entity : EntityChild
{
    public bool IsNpc => _isNpc;
    [SerializeField] protected bool _isNpc = true;

    #region Data
    [SerializeField] protected SOEntity _baseData;

    public float Health => _health;
    [SerializeField] protected float _health;

    public float MaxHealth => _maxHealth;
    [SerializeField] protected float _maxHealth;

    public float Damage => _damage;
    [SerializeField] protected float _damage;

    public float MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }
    [SerializeField] protected float _movementSpeed;

    public float AttackSpeed => _attackSpeed;
    [SerializeField] protected float _attackSpeed;
    public float AttackRange => _attackRange;
    [SerializeField] protected float _attackRange;

    public float VisionRange => _visionRange;
    [SerializeField] protected float _visionRange;

    public void SetValuesFromBaseData()
    {
        if (_baseData == null)
        {
            // NOT TESTED
            if (GameManager.Instance.EntityList.List.ContainsKey(name))
                _baseData = GameManager.Instance.EntityList.List[name];
            else
                _baseData = GameManager.Instance.EntityList.List.First().Value;
        }

        _health = _baseData.MaxHealth;
        _maxHealth = _baseData.MaxHealth;
        _damage = _baseData.Damage;
        _movementSpeed = _baseData.MovementSpeed;
        _attackSpeed = _baseData.AttackSpeed;
        _attackRange = _baseData.AttackRange;
        _visionRange = _baseData.VisionRange;
    }
    #endregion Data

    public bool IsAlive => _health > 0;
    public bool IsDead => _health <= 0;

    #region Events
    public event System.Action<float> OnHealthChanged;
    public event System.Action OnDeath;
    #endregion Events

    protected virtual void Awake()
    {
        SetValuesFromBaseData();
    }

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

    public virtual void Attack(Entity target)
    {
        target.TakeDamage(_damage);
    }

    public void OnHit()
    {
        AudioManager.PlaySFX("Hit");
        VFXManager.PlayVFX("Hit");
    }
}