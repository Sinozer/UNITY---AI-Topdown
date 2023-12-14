// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class SequencerNode : CompositeNode
{
    private int currentChild;
    
    public override void OnStart()
    {
        currentChild = 0;
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        Node child = children[currentChild];
        switch (child.OnUpdate())
        {
            case State.Running:
                return State.Running;
            case State.Success:
                currentChild++;
                break;
            case State.Failure:
                return State.Failure;
        }
        
        return currentChild == children.Count ? State.Success : State.Running;
    }
}