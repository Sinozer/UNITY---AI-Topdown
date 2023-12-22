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


    protected override void Start()
    {
        base.Start();
        
        _runner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);

    }

    protected override void Update()
    {
        base.Update();
        _canShootAtPlayer = _enemy.DistFromPlayer < Entity.AttackRange;

        if (_enemy.IsDead)
        {
            _runner.GetBlackboard().SetValue("IsDead", true);
            return;
        }
        _seePlayer = _enemy.DistFromPlayer < Entity.VisionRange;

        Player player = GameManager.Instance.Player;
        if (player != null)
            _runner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _runner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        _runner.GetBlackboard().SetValue("SeePlayer", _seePlayer);
        _runner.GetBlackboard().SetValue("CanAttack", _canShootAtPlayer);

    }
    
}