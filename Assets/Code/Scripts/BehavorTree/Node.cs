// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    public abstract class Node
    {
        protected NodeState _state;
        public NodeState State
        {
            get { return _state; }
        }

        public Node Parent = null;

        protected List<Node> _children = new();

        private Dictionary<string, object> _blackboard = new();

        public Node()
        {
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                AddChild(child);
        }

        private void AddChild(Node child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetBlackboardValue(string key, object value)
        {
            _blackboard[key] = value;
        }

        public object GetBlackboardValue(string key)
        {
            object returnValue;
            if (_blackboard.TryGetValue(key, out returnValue))
                return returnValue;

            Node node = Parent;
            while (node != null)
            {
                returnValue = node.GetBlackboardValue(key);
                if (returnValue != null)
                    return returnValue;

                node = node.Parent;
            }
            
            return null;
        }

        public bool ClearBlackboardValue(string key)
        {
            if (_blackboard.ContainsKey(key))
            {
                _blackboard.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearBlackboardValue(key);
                if (cleared == true)
                    return true;

                node = node.Parent;
            }

            return false;
        }
    }
}