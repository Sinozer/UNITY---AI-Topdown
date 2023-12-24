// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class HitterBrain : EnemyBrain
{
    protected override void Start()
    {
        base.Start();
        
        _btRunner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);

    }

    protected override void Update()
    {
        base.Update();

        if (Enemy.IsDead)
        {
            _btRunner.GetBlackboard().SetValue("IsDead", true);
            return;
        }

        Player player = GameManager.Instance.Player;

        _btRunner.GetBlackboard().SetValue("PlayerPosition", player != null ? (Vector2)player.transform.position : Vector2.zero);
        _btRunner.GetBlackboard().SetValue("SeePlayer", IsInVisionRange);
        _btRunner.GetBlackboard().SetValue("CanAttack", IsInShootRange);
    }
    
}