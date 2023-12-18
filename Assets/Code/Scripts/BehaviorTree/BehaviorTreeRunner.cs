// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviourTree Tree;

    void Start()
    {
        Tree = Tree.Clone();
        Tree.Blackboard.SetValue("Self", gameObject);
    }

    void Update()
    {
        Tree.Update();
    }

    public CustomBlackboard GetBlackboard()
    {
        return Tree.Blackboard;
    }
}