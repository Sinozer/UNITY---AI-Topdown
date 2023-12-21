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
    [SerializeField] private AudioSource _explosionSound;

    private Player player;
    private float _explosionRange = 3;


    protected override void Start()
    {
        base.Start();

        _runner.GetBlackboard().SetValue("AttackSpeed", _entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();

        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;

        if (_enemy.IsDead)
        {
            _runner.GetBlackboard().SetValue("TriggerExplosion", true);
            return;
        }

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
        if (_enemy.DistFromPlayer < _explosionRange)
            _enemy.Attack(player);

        _enemy.TakeDamage(_enemy.MaxHealth);
        _runner.GetBlackboard().SetValue("TriggerExplosion", true);
        _fxExplosion.Invoke();
        _explosionSound.Play();
    }
}