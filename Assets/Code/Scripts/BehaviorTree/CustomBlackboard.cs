// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;

public class CustomBlackboard : ScriptableObject
{
    Dictionary<string, float> _floatData;
    Dictionary<string, int> _intData;
    Dictionary<string, bool> _boolData;
    Dictionary<string, Vector2> _vector2Data;
    Dictionary<string, Vector3> _vector3Data;
    Dictionary<string, GameObject> _gameObjectData;
    Dictionary<string, string> _stringData;

    public CustomBlackboard Clone()
    {
        return Instantiate(this);
    }

    public bool TryFind<T>(string key, out T value)
    {
        Dictionary<string, T> data = null;

        if (typeof(T) == typeof(float))
            data = _floatData as Dictionary<string, T>;
        else if (typeof(T) == typeof(int))
            data = _intData as Dictionary<string, T>;
        else if (typeof(T) == typeof(bool))
            data = _boolData as Dictionary<string, T>;
        else if (typeof(T) == typeof(Vector2))
            data = _vector2Data as Dictionary<string, T>;
        else if (typeof(T) == typeof(Vector3))
            data = _vector3Data as Dictionary<string, T>;
        else if (typeof(T) == typeof(GameObject))
            data = _gameObjectData as Dictionary<string, T>;
        else if (typeof(T) == typeof(string))
            data = _stringData as Dictionary<string, T>;

        if (data != null && data.ContainsKey(key))
        {
            value = data[key];
            return true;
        }
        value = default(T);
        return false;
    }
}
