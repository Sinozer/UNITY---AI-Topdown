// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public class DebugLogNode : ActionNode
{
    public string Message;
    
    public override void OnStart()
    {
        Debug.Log($"OnStart: {Message}");
    }

    public override void OnStop()
    {
        Debug.Log($"OnStop: {Message}");
    }

    public override State OnUpdate()
    {
        Debug.Log($"OnUpdate: {Message}");
        return State.Success;
    }
}