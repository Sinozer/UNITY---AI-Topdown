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
        GameObject parentGameObject = transform.parent.gameObject;
        Tree = Tree.Clone();
        //Tree.Blackboard.SetValue("Self", gameObject);
        Tree.Blackboard.SetValue("Self", parentGameObject);
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