// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public class WaitNode : ActionNode
{
    public float Duration = 1;
    private float _startTime;
    
    public override void OnStart()
    {
        _startTime = Time.time;
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        return Time.time - _startTime > Duration ? State.Success : State.Running;
    }
}