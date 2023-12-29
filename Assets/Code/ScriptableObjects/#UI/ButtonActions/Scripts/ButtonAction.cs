// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[System.Serializable]
public struct ButtonActionData
{
    public int IntData;
    public string StringData;
    public float FloatData;
    public bool BoolData;
}

public abstract class ButtonAction : ScriptableObject
{
    public abstract void Execute(ButtonActionData data = default);
}