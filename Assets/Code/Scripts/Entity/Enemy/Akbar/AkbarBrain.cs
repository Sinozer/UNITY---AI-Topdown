// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AkbarBrain : EnemyBrain
{
    public readonly float ExplosionRange = 3;

    protected override void Start()
    {
        base.Start();
        BTRunner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();

        if (Dead)
        {
            BTRunner.GetBlackboard().SetValue("IsDead", true);
            return;
        }

        BTRunner.GetBlackboard().SetValue("IsInVisionRange", IsInVisionRange);
        BTRunner.GetBlackboard().SetValue("IsInShootRange", IsInShootRange);
    }
}