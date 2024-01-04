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
        BTRunner.GetBlackboard().SetValue("AttackSpeed", Entity.Data.GetValue<float>("AttackSpeed"));
    }

    protected override void Update()
    {
        base.Update();

        if (Dead)
        {
            BTRunner.GetBlackboard().SetValue("IsDead", true);
            return;
        }

        if (Alive)
        {
            BTRunner.GetBlackboard().SetValue("IsInVisionRange", IsInVisionRange);
            BTRunner.GetBlackboard().SetValue("IsInShootRange", IsInShootRange);
            BTRunner.GetBlackboard().SetValue("IsInShootCooldown", ShootAction.IsInShootCooldown);
        }
    }
    
}