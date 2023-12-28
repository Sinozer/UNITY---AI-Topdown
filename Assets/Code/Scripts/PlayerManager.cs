// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Reference<SOEntity> Data => _data;
    private Reference<SOEntity> _data;

    public Reference<float> Health => _health;
    private Reference<float> _health;

    protected override void Awake()
    {
        base.Awake();

        _data = ScriptableObject.CreateInstance<DataReference>();
        _health = ScriptableObject.CreateInstance<FloatReference>();
    }

#if UNITY_EDITOR
    [Button]
    private void DebugLog()
    {
        Debug.Log($"Health: {_health.Acquire()}");

        Debug.Log($"MaxHealth: {_data.Acquire().MaxHealth}");
        Debug.Log($"Damage: {_data.Acquire().Damage}");
        Debug.Log($"Speed: {_data.Acquire().MovementSpeed}");
        Debug.Log($"AttackSpeed: {_data.Acquire().AttackSpeed}");
        Debug.Log($"AttackRange: {_data.Acquire().AttackRange}");
        Debug.Log($"VisionRange: {_data.Acquire().VisionRange}");
    }
#endif
}