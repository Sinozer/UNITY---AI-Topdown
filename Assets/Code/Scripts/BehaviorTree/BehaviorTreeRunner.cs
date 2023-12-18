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
    public Blackboard Blackboard;

    void Start()
    {
        Tree = Tree.Clone();
    }

    void Update()
    {
        Tree.Update();
    }
}