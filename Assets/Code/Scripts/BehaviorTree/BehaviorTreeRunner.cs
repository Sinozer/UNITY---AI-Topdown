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

    // Blackboard Test
    //protected virtual void Start()
    //{
    //    Tree.Blackboard.SetValue("AFloat", 10f);
    //    Tree.Blackboard.SetValue("AInt", 1);
    //    Tree.Blackboard.SetValue("ABool", true);
    //    Tree.Blackboard.SetValue("AVector2", Vector2.one);
    //    Tree.Blackboard.SetValue("AVector3", Vector3.one);
    //    Tree.Blackboard.SetValue("AGameObject", gameObject);
    //    Tree.Blackboard.SetValue("AString", "AString");
    //    Tree.Blackboard.SetValue("ATransform", transform);

    //    if (Tree.Blackboard.TryFind("AFloat", out float aFloat))
    //        Debug.Log(aFloat);
    //    if (Tree.Blackboard.TryFind("AInt", out int anInt))
    //        Debug.Log(anInt);
    //    if (Tree.Blackboard.TryFind("ABool", out bool aBool))
    //        Debug.Log(aBool);
    //    if (Tree.Blackboard.TryFind("AVector2", out Vector2 aVec2))
    //        Debug.Log(aVec2);
    //    if (Tree.Blackboard.TryFind("AVector3", out Vector3 aVec3))
    //        Debug.Log(aVec3);
    //    if (Tree.Blackboard.TryFind("AGameObject", out GameObject go))
    //        Debug.Log(go);
    //    if (Tree.Blackboard.TryFind("AString", out string aString))
    //        Debug.Log(aString);
    //    if (Tree.Blackboard.TryFind("ATransform", out Transform aTransform))
    //        Debug.Log(aTransform);
    //}

    protected virtual void Update()
    {
        Tree.Update();
    }

    public CustomBlackboard GetBlackboard()
    {
        return Tree.Blackboard;
    }
}