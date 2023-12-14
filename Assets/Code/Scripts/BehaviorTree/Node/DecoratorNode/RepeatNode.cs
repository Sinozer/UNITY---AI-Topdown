// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class RepeatNode : DecoratorNode
{
    public override void OnStart()
    {
        
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        Child.Update();
        return State.Running;
    }
}