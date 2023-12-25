// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public class RangerBrain : EnemyBrain
{
    protected override void Start()
    {
        base.Start();
        _btRunner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();

        if (Alive)
        {
            _btRunner.GetBlackboard().SetValue("IsInVisionRange", IsInVisionRange);
            _btRunner.GetBlackboard().SetValue("IsInShootRange", IsInShootRange);
            _btRunner.GetBlackboard().SetValue("IsInShootCooldown", _entityShooting.IsInShootCooldown);
        }

        if (Dead)
        {
            _btRunner.GetBlackboard().SetValue("IsDead", true);
            _btRunner.GetBlackboard().SetValue("IsInVisionRange", false);
            _btRunner.GetBlackboard().SetValue("IsInShootRange", false);
            _btRunner.GetBlackboard().SetValue("IsInShootCooldown", false);
        }
    }
}
