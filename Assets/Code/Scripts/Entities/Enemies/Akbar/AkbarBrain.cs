// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Events;

public class AkbarBrain : EnemyBrain
{
    [SerializeField] private EnemyBTRunner _runner;
    [SerializeField] private UnityEvent _fxExplosion;

    private Player player;

    protected override void Start()
    {
        base.Start();

        _runner.GetBlackboard().SetValue("AttackSpeed", _entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();
        _runner.GetBlackboard().SetValue("IsDead", _enemy.IsDead);
        if (_enemy.IsDead)
        {
            Explode();
            return;
        }
        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;

        player = GameManager.Instance.Player;
        if (player != null)
            _runner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _runner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        _runner.GetBlackboard().SetValue("SeePlayer", _seePlayer);
        _runner.GetBlackboard().SetValue("CanShoot", _canShootAtPlayer);
    }

    public void Explode()
    {
        _fxExplosion.Invoke();
    }
}