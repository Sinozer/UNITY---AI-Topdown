// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

// <summary>
// This class is responsible for running a behavior tree.
// You can derive from this class to init some blackboard values.
// You shouldn't define the Awake or Update methods in your derived class.
// If you ever want to define these methods, make sure to call the base method.
// </summary>
public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviourTree Tree;

    protected virtual void Awake()
    {
        Tree = Tree.Clone();
        Tree.Blackboard.SetValue("Self", gameObject);
    }

    protected virtual void Update()
    {
        Tree.Update();
    }

    public CustomBlackboard GetBlackboard()
    {
        return Tree.Blackboard;
    }
}