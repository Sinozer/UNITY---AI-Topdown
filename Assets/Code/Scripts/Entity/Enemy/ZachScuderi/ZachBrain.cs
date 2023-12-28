// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class ZachBrain : EnemyBrain
{
    //private float MeleeRange => Entity.AttackRange * 0.5f;
    //public bool CanMeleeAttack => Enemy.DistFromPlayer < MeleeRange;
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

        if (Alive)
        {
            BTRunner.GetBlackboard().SetValue("IsInVisionRange", IsInVisionRange);
            BTRunner.GetBlackboard().SetValue("IsInShootRange", IsInShootRange);
            BTRunner.GetBlackboard().SetValue("IsInShootCooldown", ShootAction.IsInShootCooldown);
        }

        //if (Dead)
        //{
        //    BTRunner.GetBlackboard().SetValue("IsDead", true);
        //    BTRunner.GetBlackboard().SetValue("IsInVisionRange", false);
        //    BTRunner.GetBlackboard().SetValue("IsInShootRange", false);
        //    BTRunner.GetBlackboard().SetValue("IsInShootCooldown", false);
        //}
    }
}
