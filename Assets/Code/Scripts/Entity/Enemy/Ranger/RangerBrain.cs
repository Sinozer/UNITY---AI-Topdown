// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class RangerBrain : EnemyBrain
{
    [SerializeField] private EnemyBTRunner _btRunner;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (Alive)
        {
            _btRunner?.GetBlackboard().SetValue("SeePlayer", SeePlayer);
            _btRunner?.GetBlackboard().SetValue("CanShoot", CanShootAtPlayer);
        }

        if (Dead)
        {
            _btRunner?.GetBlackboard().SetValue("IsDead", true);
            _btRunner?.GetBlackboard().SetValue("SeePlayer", false);
            _btRunner?.GetBlackboard().SetValue("CanShoot", false);
        }
    }

}
