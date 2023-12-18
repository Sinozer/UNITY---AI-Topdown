// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviourTree Tree;
    public CustomBlackboard Blackboard;

    void Start()
    {
        Tree = Tree.Clone();
        Blackboard = Blackboard.Clone();
    }

    void Update()
    {
        Tree.Update();
    }
}