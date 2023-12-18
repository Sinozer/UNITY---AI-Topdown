// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class PatrolNode : DecoratorNode
{
    private GameObject _self;
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