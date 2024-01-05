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

    public void SetValuesFromBaseData(bool reset = false)
    {
        if (_baseData == null)
            return;

        if (reset)
        {
            Data.SetValue<float>("Health", _baseData.MaxHealth);
            Data.SetValue<float>("MaxHealth", _baseData.MaxHealth);
            Data.SetValue<float>("Damage", _baseData.Damage);
            Data.SetValue<float>("MovementSpeed", _baseData.MovementSpeed);
            Data.SetValue<float>("AttackSpeed", _baseData.AttackSpeed);
            Data.SetValue<float>("AttackRange", _baseData.AttackRange);
            Data.SetValue<float>("VisionRange", _baseData.VisionRange);
        }
        else
        {
            Data.SetValueIfNotExists<float>("Health", _baseData.MaxHealth);
            Data.SetValueIfNotExists<float>("MaxHealth", _baseData.MaxHealth);
            Data.SetValueIfNotExists<float>("Damage", _baseData.Damage);
            Data.SetValueIfNotExists<float>("MovementSpeed", _baseData.MovementSpeed);
            Data.SetValueIfNotExists<float>("AttackSpeed", _baseData.AttackSpeed);
            Data.SetValueIfNotExists<float>("AttackRange", _baseData.AttackRange);
            Data.SetValueIfNotExists<float>("VisionRange", _baseData.VisionRange);
        }
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