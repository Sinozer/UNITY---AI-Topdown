// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;

namespace BehaviorTree
{
    public abstract class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        _state = NodeState.SUCCESS;
                        return _state;
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = NodeState.FAILURE;
            return _state;
        }
    }
}