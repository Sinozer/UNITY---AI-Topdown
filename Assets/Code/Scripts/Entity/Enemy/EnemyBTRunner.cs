// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class EnemyBTRunner : BehaviorTreeRunner
{
    public EnemyBrain EnemyBrain
    {
        get
        {
            if (_enemyBrain == null)
                _enemyBrain = transform.root.GetComponentInChildren<EnemyBrain>();

            return _enemyBrain;
        }
    }
    private EnemyBrain _enemyBrain;

    void Start()
    {
        Tree.Blackboard.SetValue("EnemyBrain", EnemyBrain);
    }
}
