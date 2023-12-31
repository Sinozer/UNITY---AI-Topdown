// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/CustomBlackboard")]
[System.Serializable]
public class CustomBlackboard : SerializedScriptableObject
{
    [SerializeField] private Dictionary<string, float> _floatData = new Dictionary<string, float>();
    [SerializeField] private Dictionary<string, int> _intData = new Dictionary<string, int>();
    [SerializeField] private Dictionary<string, bool> _boolData = new Dictionary<string, bool>();
    [SerializeField] private Dictionary<string, Vector2> _vector2Data = new Dictionary<string, Vector2>();
    [SerializeField] private Dictionary<string, Vector3> _vector3Data = new Dictionary<string, Vector3>();
    [SerializeField] private Dictionary<string, Transform> _transformData = new Dictionary<string, Transform>();
    [SerializeField] private Dictionary<string, GameObject> _gameObjectData = new Dictionary<string, GameObject>();
    [SerializeField] private Dictionary<string, string> _stringData = new Dictionary<string, string>();
    [SerializeField] private Dictionary<string, object> _objectData = new Dictionary<string, object>();

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
            { typeof(string), _stringData },
            { typeof(Transform), _transformData },
        };
    }

    public bool TryFind<T>(string key, out T value)
    {
        value = default(T);

        if (!_typeMap.ContainsKey(typeof(T)))
        {
            if (!_objectData.ContainsKey(key))
                return false;

            object obj = _objectData[key];
            if (obj is T castedValue)
            {
                value = castedValue;
                return true;
            } 
            else
                return false;
        }

        Dictionary<string, T> data = _typeMap[typeof(T)] as Dictionary<string, T>;

        if (!data.ContainsKey(key))
            return false;

        value = data[key];
        return true;
    }

    public void SetValue<T>(string key, T value)
    {
        if (!_typeMap.ContainsKey(typeof(T))) 
        {
            _objectData[key] = value;
            return;
        }

        Dictionary<string, T> data = _typeMap[typeof(T)] as Dictionary<string, T>;

        data[key] = value;
    }
}