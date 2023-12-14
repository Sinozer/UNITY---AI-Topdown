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
    private int _currentChild;
    
    public override void OnStart()
    {
        _currentChild = 0;
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        Node child = Children[_currentChild];
        switch (child.OnUpdate())
        {
            case State.Running:
                return State.Running;
            case State.Success:
                _currentChild++;
                break;
            case State.Failure:
                return State.Failure;
        }
        
        return _currentChild == Children.Count ? State.Success : State.Running;
    }
}