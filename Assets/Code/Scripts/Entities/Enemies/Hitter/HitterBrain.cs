// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class HitterBrain : EnemyBrain
{
    [SerializeField] EnemyBTRunner _runner;
    
    private void Update()
    {
        _enemy.DistFromPlayer = _enemy.CalculateDistFromPlayer();
        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;

        Player player = GameManager.Instance.Player;
        if (player != null)
            _runner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _runner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        _runner.GetBlackboard().SetValue("SeePlayer", _seePlayer);
    }
    
}