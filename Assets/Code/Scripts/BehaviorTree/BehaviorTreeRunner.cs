// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    private BehaviourTree _tree;
    void Start()
    {
        _tree = ScriptableObject.CreateInstance<BehaviourTree>();
        
        DebugLogNode log = ScriptableObject.CreateInstance<DebugLogNode>();
        log.Message = "League of Legends is a !good game";

        RepeatNode loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.Child = log;
        
        _tree.RootNode = loop;
    }

    void Update()
    {
        _tree.Update();
    }
}