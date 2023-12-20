// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EnemyBTRunner : BehaviorTreeRunner
{
    [SerializeField] private EnemyBrain _enemyBrain;

    void Start()
    {
        Tree.Blackboard.SetValue("EnemyBrain", _enemyBrain);
    }
}
