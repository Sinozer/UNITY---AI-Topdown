// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //
using UnityEngine;


public class RangerBrain : EnemyBrain
{

    [SerializeField] EnemyBTRunner _runner;
    private CustomBlackboard _blackboard;
    private Player _player;

    protected override void Start()
    {
        base.Start();
        _blackboard = _runner.GetBlackboard();
    }

    private void Update()
    {
        _enemy.DistFromPlayer = _enemy.CalculateDistFromPlayer();

        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;

        if (_player == null)
        {
            _player = GameManager.Instance.Player;
            _blackboard.SetValue("PlayerPosition", Vector2.zero);
            return;
        }
        _blackboard.SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        _blackboard.SetValue("SeePlayer", _seePlayer);
        _blackboard.SetValue("CanShoot", _canShootAtPlayer);
    }

    private void FixedUpdate()
    {
        _blackboard.SetValue("Health", _entity.Health);
    }
}
