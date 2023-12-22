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

        if (Enemy.IsDead)
        {
            _runner.GetBlackboard().SetValue("IsDead", true);
            return;
        }

        Player player = GameManager.Instance.Player;

        _runner.GetBlackboard().SetValue("PlayerPosition", player != null ? (Vector2)player.transform.position : Vector2.zero);
        _runner.GetBlackboard().SetValue("SeePlayer", SeePlayer);
        _runner.GetBlackboard().SetValue("CanAttack", CanShootAtPlayer);

    }
    
}