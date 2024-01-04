// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;
using UnityEngine;

public abstract class Entity : EntityChild
{
    public bool IsNpc => _isNpc;
    [SerializeField] protected bool _isNpc = true;

    #region Data
    [OdinSerialize]
    public bool InstantiateData { get; private set; } = true;
    [SerializeField, InlineEditor] private CustomBlackboard _data;
    public CustomBlackboard Data { get => _data; private set => _data = value; }

    [SerializeField, DisableInEditorMode] protected SOEntity _baseData;

    //public float Health => _health;
    //[SerializeField, DisableInEditorMode, ProgressBar(0, "@_maxHealth", ColorGetter = "@Color.Lerp(Color.red, Color.green, Mathf.Pow(_health / _maxHealth, 2))")] protected float _health;

    //public float MaxHealth => _maxHealth;
    //[SerializeField, DisableInEditorMode] protected float _maxHealth;

    //public float Damage => _damage;
    //[SerializeField, DisableInEditorMode] protected float _damage;

    //public float MovementSpeed
    //{
    //    get => _movementSpeed;
    //    set => _movementSpeed = value;
    //}
    //[SerializeField, DisableInEditorMode] protected float _movementSpeed;

    //public float AttackSpeed => _attackSpeed;
    //[SerializeField, DisableInEditorMode] protected float _attackSpeed;
    //public float AttackRange => _attackRange;
    //[SerializeField, DisableInEditorMode] protected float _attackRange;

    //public float VisionRange => _visionRange;
    //[SerializeField, DisableInEditorMode] protected float _visionRange;

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

        if (_data == null)
            _data = ScriptableObject.CreateInstance<CustomBlackboard>();
        else if (InstantiateData)
            _data = Instantiate(_data);
        else
            _data.Awake();

        Data.SetValueIfNotExists<float>("Health", _baseData.MaxHealth);
        Data.SetValueIfNotExists<float>("MaxHealth", _baseData.MaxHealth);
        Data.SetValueIfNotExists<float>("Damage", _baseData.Damage);
        Data.SetValueIfNotExists<float>("MovementSpeed", _baseData.MovementSpeed);
        Data.SetValueIfNotExists<float>("AttackSpeed", _baseData.AttackSpeed);
        Data.SetValueIfNotExists<float>("AttackRange", _baseData.AttackRange);
        Data.SetValueIfNotExists<float>("VisionRange", _baseData.VisionRange);
    }
    #endregion Data

    public bool IsAlive
    {
        get
        {
            if (Data.TryFind<float>("Health", out float health))
                return health > 0;

            return false;

        }
    }
    public bool IsDead => !IsAlive;

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
        if (Data.TryFind<float>("Health", out float health) == false || Data.TryFind<float>("MaxHealth", out float maxHealth) == false)
            return;

        Data.SetValue<float>("Health", Mathf.Clamp(health + healAmount, 0, maxHealth));

        OnHealthChanged?.Invoke(Data.GetValue<float>("Health"));
    }

    public virtual void TakeDamage(float damage)
    {
        if (Data.TryFind<float>("Health", out float health) == false || Data.TryFind<float>("MaxHealth", out float maxHealth) == false)
            return;

        AudioManager.PlaySFX("Hit");
        VFXManager.PlayVFX("Hit");

        Data.SetValue<float>("Health", Mathf.Clamp(health - damage, 0, maxHealth));

        OnHealthChanged?.Invoke(Data.GetValue<float>("Health"));

        if (IsDead)
            OnDeath?.Invoke();
    }

    public virtual void Attack(Entity target)
    {
        target.TakeDamage(Data.GetValue<float>("Damage"));
    }
}