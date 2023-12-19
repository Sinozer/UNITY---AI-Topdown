// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/CustomBlackboard")]
[System.Serializable]
public class CustomBlackboard : ScriptableObject
{
    [SerializeField] private SerializedDictionary<string, float> _floatData;
    [SerializeField] private SerializedDictionary<string, int> _intData;
    [SerializeField] private SerializedDictionary<string, bool> _boolData;
    [SerializeField] private SerializedDictionary<string, Vector2> _vector2Data;
    [SerializeField] private SerializedDictionary<string, Vector3> _vector3Data;
    [SerializeField] private SerializedDictionary<string, GameObject> _gameObjectData;
    [SerializeField] private SerializedDictionary<string, string> _stringData;

    private Dictionary<System.Type, object> _typeMap = new Dictionary<System.Type, object>();

    public CustomBlackboard Clone()
    {
        return Instantiate(this);
    }

    private void Awake()
    {
        _typeMap = new Dictionary<System.Type, object>
        {
            { typeof(float), _floatData },
            { typeof(int), _intData },
            { typeof(bool), _boolData },
            { typeof(Vector2), _vector2Data },
            { typeof(Vector3), _vector3Data },
            { typeof(GameObject), _gameObjectData },
            { typeof(string), _stringData }
        };
    }

    public bool TryFind<T>(string key, out T value)
    {
        if (!_typeMap.ContainsKey(typeof(T)))
        {
            value = default(T);
            return false;
        }

        Dictionary<string, T> data = _typeMap[typeof(T)] as Dictionary<string, T>;

        if (data == null || !data.ContainsKey(key))
        {
            value = default(T);
            return false;
        }

        value = data[key];
        return true;
    }

    public bool SetValue<T>(string key, T value)
    {
        if (!_typeMap.ContainsKey(typeof(T)))
            return false;

        Dictionary<string, T> data = _typeMap[typeof(T)] as Dictionary<string, T>;

        if (data == null)
            return false;

        data[key] = value;
        return true;
    }
}
