// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AkbarBrain : EnemyBrain
{
    [SerializeField] BehaviorTreeRunner _runner;
    CustomBlackboard _blackboard;

    private void Update()
    {
        _enemy.DistFromPlayer = _enemy.CalculateDistFromPlayer();

        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;

        _runner.GetBlackboard().SetValue("SeePlayer", _seePlayer);
        if(_seePlayer)
        {
            _runner.GetBlackboard().SetValue<Vector2>("PlayerPos", _enemy.GetPlayerPos());
        }
    }
}