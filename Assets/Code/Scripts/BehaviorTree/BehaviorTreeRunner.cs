// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviourTree _tree;
    void Start()
    {
        _tree = _tree.Clone();
    }

    void Update()
    {
        _tree.Update();
    }
}