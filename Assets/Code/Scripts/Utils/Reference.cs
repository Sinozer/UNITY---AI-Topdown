// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public interface IReferenceHead<T>
{
    void Set(T v);
}

public class Reference<T> : ScriptableObject, IReferenceHead<T>
{
    private T _value { get; set; }

    public event Action<T> OnValueChanged;

    public T Acquire() => _value;

    public void Set(T v)
    {
        _value = v;
        OnValueChanged?.Invoke(_value);
    }
}

public class FloatReference : Reference<float> { }
public class IntReference : Reference<int> { }
public class BoolReference : Reference<bool> { }
public class StringReference : Reference<string> { }
public class DataReference : Reference<SOEntity> { }