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
    [SerializeField] private bool executeAllEachFrame = false;  // Default to false to maintain current behavior

    public override void OnStart()
    {
        _currentChild = 0;
    }

    public override void OnStop()
    {
        if (CurrentState == State.Failure)
        {
            foreach (var child in Children)
            {
                if (child.CurrentState != State.Running)
                    continue;

                child.OnStop();
                child.Started = false;
            }
        }
    }

    public override State OnUpdate()
    {
        if (!executeAllEachFrame)
        {
            return UpdateSingleNode();
        }
        else
        {
            return UpdateAllNodes();
        }
    }

    private State UpdateSingleNode()
    {
        Node child = Children[_currentChild];
        switch (child.Update())
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

    private State UpdateAllNodes()
    {
        foreach (Node child in Children)
        {
            switch (child.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Failure;
            }
        }

        return State.Success;
    }
}
