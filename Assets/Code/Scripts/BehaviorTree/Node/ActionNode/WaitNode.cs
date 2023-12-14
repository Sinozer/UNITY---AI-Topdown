// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration = 1;
    private float startTime;
    
    public override void OnStart()
    {
        startTime = Time.time;
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        return Time.time - startTime > duration ? State.Success : State.Running;
    }
}