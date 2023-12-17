// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public class RootNode : Node
{
    [HideInInspector] public Node Child;
    
    public override void OnStart()
    {
        
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        return Child.Update();
    }
    
    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.Child = Child.Clone();
        return node;
    }
}