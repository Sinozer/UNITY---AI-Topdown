// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    private BehaviourTree tree;
    void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaviourTree>();
        
        DebugLogNode log = ScriptableObject.CreateInstance<DebugLogNode>();
        log.message = "League of Legends is a !good game";

        RepeatNode loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.child = log;
        
        tree.rootNode = loop;
    }

    void Update()
    {
        tree.Update();
    }
}