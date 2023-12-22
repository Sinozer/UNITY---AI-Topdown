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
    
    private Player _player;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (_enemy.IsDead)
            return;

        _canShootAtPlayer = _enemy.DistFromPlayer < Entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < Entity.VisionRange;

        _player = GameManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        if (!IsDead)
        {
                _btRunner?.GetBlackboard().SetValue("SeePlayer", _seePlayer);
                _btRunner?.GetBlackboard().SetValue("CanShoot", _canShootAtPlayer);
        }

        if (IsDead)
        {
            _btRunner?.GetBlackboard().SetValue("IsDead", true);
            _btRunner?.GetBlackboard().SetValue("SeePlayer", false);
            _btRunner?.GetBlackboard().SetValue("CanShoot", false);
        }
    }

}
