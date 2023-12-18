// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using AYellowpaper.SerializedCollections;
using Sirenix.OdinInspector.Editor.TypeSearch;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/CustomBlackboard")]
[System.Serializable]
public class CustomBlackboard : ScriptableObject
{
    [SerializeField] SerializedDictionary<string, float> _floatData;
    [SerializeField] SerializedDictionary<string, int> _intData;
    [SerializeField] SerializedDictionary<string, bool> _boolData;
    [SerializeField] SerializedDictionary<string, Vector2> _vector2Data;
    [SerializeField] SerializedDictionary<string, Vector3> _vector3Data;
    [SerializeField] SerializedDictionary<string, GameObject> _gameObjectData;
    [SerializeField] SerializedDictionary<string, string> _stringData;

    private SerializedDictionary<System.Type, object> _typeMap;

    public CustomBlackboard Clone()
    {
        return Instantiate(this);
    }

    private void Awake()
    {
        //Debug.Log("Awake");
        _typeMap = new SerializedDictionary<System.Type, object>
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
        Dictionary<string, T> data = null;

        if (typeof(T) == typeof(float))
            data = _floatData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(int))
            data = _intData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(bool))
            data = _boolData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(Vector2))
            data = _vector2Data as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(Vector3))
            data = _vector3Data as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(GameObject))
            data = _gameObjectData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(string))
            data = _stringData as SerializedDictionary<string, T>;

        if (data != null && data.ContainsKey(key))
        {
            value = data[key];
            return true;
        }
        value = default(T);
        return false;
    }

    public bool SetValue<T>(string key, T value)
    {
        Dictionary<string, T> data = null;

        if (typeof(T) == typeof(float))
            data = _floatData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(int))
            data = _intData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(bool))
            data = _boolData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(Vector2))
            data = _vector2Data as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(Vector3))
            data = _vector3Data as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(GameObject))
            data = _gameObjectData as SerializedDictionary<string, T>;
        else if (typeof(T) == typeof(string))
            data = _stringData as SerializedDictionary<string, T>;

        if (data != null)
        {
            data[key] = value;
            return true;
        }
        return false;
    }
}


//{
//    [SerializeField] SerializedDictionary<string, object> _data;

//    public CustomBlackboard()
//    {
//        _data = new SerializedDictionary<string, object>();
//    }

//    public void SetValue<T>(string key, T value)
//    {
//        _data[key] = value;
//    }

//    public bool TryGetValue<T>(string key, out T value)
//    {
//        if (_data.TryGetValue(key, out object objValue) && objValue is T)
//        {
//            value = (T)objValue;
//            return true;
//        }

//        value = default(T);
//        return false;
//    }

//    public CustomBlackboard Clone()
//    {
//        return Instantiate(this);
//    }
//}


