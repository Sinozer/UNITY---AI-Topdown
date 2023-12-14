// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;
    
    public override void OnStart()
    {
        Debug.Log($"OnStart: {message}");
    }

    public override void OnStop()
    {
        Debug.Log($"OnStop: {message}");
    }

    public override State OnUpdate()
    {
        Debug.Log($"OnUpdate: {message}");
        return State.Success;
    }
}