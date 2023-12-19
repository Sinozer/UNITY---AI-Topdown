// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ZachBehaviorRunner : MonoBehaviour
{
    public BehaviourTree Tree;

    private Enemy _enemyData;
    void Start()
    {
        GameObject parentGameObject = transform.parent.gameObject;
        _enemyData = parentGameObject.GetComponent<Enemy>();
        Tree = Tree.Clone();
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