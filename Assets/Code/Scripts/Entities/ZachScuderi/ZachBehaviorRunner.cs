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

    private Enemy _data;
    void Start()
    {
        GameObject parentGameObject = transform.parent.gameObject;
        _data = parentGameObject.GetComponentInParent<Enemy>();
        Tree = Tree.Clone();
        Tree.Blackboard.SetValue("Self", parentGameObject);
        SetDataToBlackboard();
    }

    void Update()
    {
        Tree.Update();
    }

    public CustomBlackboard GetBlackboard()
    {
        return Tree.Blackboard;
    }
    
    private void SetDataToBlackboard()
    {   
        Tree.Blackboard.SetValue("Health", _data.Health);
        Tree.Blackboard.SetValue("MaxHealth", _data.MaxHealth);
        Tree.Blackboard.SetValue("Damage", _data.Damage);
        Tree.Blackboard.SetValue("MovementSpeed", _data.MovementSpeed);
        Tree.Blackboard.SetValue("AttackSpeed", _data.AttackSpeed);
        Tree.Blackboard.SetValue("AttackRange", _data.AttackRange);
        Tree.Blackboard.SetValue("VisionRange", _data.VisionRange);
        Tree.Blackboard.SetValue("DistFromPlayer", _data.DistFromPlayer);
    }
    
}