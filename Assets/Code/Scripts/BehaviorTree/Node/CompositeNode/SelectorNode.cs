// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //
using Unity.VisualScripting;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    private int _currentChild;
    [SerializeField] private bool executeAllEachFrame = false;

    public override void OnStart()
    {
        _currentChild = 0;
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        return !executeAllEachFrame ? UpdateSingleNode() : UpdateAllNodes();
    }

    private State UpdateSingleNode()
    {
        switch (Children[_currentChild].Update())
        {
            case State.Running:
                return State.Running;
            case State.Success:
                return State.Success;
            case State.Failure:
                _currentChild++;
                break;
        }

        return _currentChild < Children.Count ? State.Running : State.Failure;
    }

    private State UpdateAllNodes()
    {
        foreach(Node child in Children)
        {
            switch (child.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    return State.Success;
            }
        }

        return State.Failure;
    }
}
