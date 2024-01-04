// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // This is only called if this script is instantiated. Call this function if you want to initialize the blackboard without making a clone.
    public void Awake()
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

    public T GetValue<T>(string key)
    {
        if (!_typeMap.ContainsKey(typeof(T)))
        {
            if (!_objectData.ContainsKey(key))
                throw new System.Exception($"Key {key} not found in blackboard");

            object obj = _objectData[key];
            if (obj is T castedValue)
                return castedValue;
            else
                throw new System.Exception($"Key {key} type mismatch. Expected {typeof(T)} but found {obj.GetType()}");
        }

        Dictionary<string, T> data = _typeMap[typeof(T)] as Dictionary<string, T>;

        if (!data.ContainsKey(key))
            throw new System.Exception($"Key {key} not found in blackboard");

        return data[key];
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

    public bool SetValueIfNotExists<T>(string key, T value)
    {
        if (!_typeMap.ContainsKey(typeof(T)))
        {
            if (!_objectData.ContainsKey(key))
            {
                _objectData[key] = value;
                return true;
            }
            else
                return false;
        }

        Dictionary<string, T> data = _typeMap[typeof(T)] as Dictionary<string, T>;

        if (!data.ContainsKey(key))
        {
            data[key] = value;
            return true;
        }
        else
            return false;
    }

#if UNITY_EDITOR
    [Button]
    private void ResetToDefault()
    {
        // For each dictionary, loop through all the keys and set the value to default(T) then save the asset.

        for (int i = 0; i < _floatData.Count; i++)
        {
            string key = _floatData.Keys.ElementAt(i);
            _floatData[key] = default(float);
        }

        for (int i = 0; i < _intData.Count; i++)
        {
            string key = _intData.Keys.ElementAt(i);
            _intData[key] = default(int);
        }

        for (int i = 0; i < _boolData.Count; i++)
        {
            string key = _boolData.Keys.ElementAt(i);
            _boolData[key] = default(bool);
        }

        for (int i = 0; i < _vector2Data.Count; i++)
        {
            string key = _vector2Data.Keys.ElementAt(i);
            _vector2Data[key] = default(Vector2);
        }

        for (int i = 0; i < _vector3Data.Count; i++)
        {
            string key = _vector3Data.Keys.ElementAt(i);
            _vector3Data[key] = default(Vector3);
        }

        for (int i = 0; i < _transformData.Count; i++)
        {
            string key = _transformData.Keys.ElementAt(i);
            _transformData[key] = default(Transform);
        }

        for (int i = 0; i < _gameObjectData.Count; i++)
        {
            string key = _gameObjectData.Keys.ElementAt(i);
            _gameObjectData[key] = default(GameObject);
        }

        for (int i = 0; i < _stringData.Count; i++)
        {
            string key = _stringData.Keys.ElementAt(i);
            _stringData[key] = default(string);
        }

        for (int i = 0; i < _objectData.Count; i++)
        {
            string key = _objectData.Keys.ElementAt(i);
            _objectData[key] = default(object);
        }

        UnityEditor.EditorUtility.SetDirty(this);

        UnityEditor.AssetDatabase.SaveAssets();

        UnityEditor.AssetDatabase.Refresh();

        UnityEditor.EditorUtility.FocusProjectWindow();
    }
#endif
}