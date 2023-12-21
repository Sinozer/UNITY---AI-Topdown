// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;

public class ZachBrain : EnemyBrain
{
    [SerializeField] EnemyBTRunner _runner;
    
    protected override void Start()
    {
        base.Start();
        _runner.GetBlackboard().SetValue("AttackSpeed", _entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();
        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;

        Player player = GameManager.Instance.Player;
        if (player != null)
            _runner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _runner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        _runner.GetBlackboard().SetValue("SeePlayer", _seePlayer);
        _runner.GetBlackboard().SetValue("CanAttack", _canShootAtPlayer);
    }
}
