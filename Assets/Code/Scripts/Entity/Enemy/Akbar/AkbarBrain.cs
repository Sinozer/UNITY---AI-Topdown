// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AkbarBrain : EnemyBrain
{
    public Player Player => GameManager.Instance.Player;
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
            BTRunner.GetBlackboard().SetValue("TriggerExplosion", true);
            return;
        }

        if (Player != null)
            BTRunner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            BTRunner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        BTRunner.GetBlackboard().SetValue("SeePlayer", IsInVisionRange);
        BTRunner.GetBlackboard().SetValue("CanShoot", IsInShootRange);
    }
}