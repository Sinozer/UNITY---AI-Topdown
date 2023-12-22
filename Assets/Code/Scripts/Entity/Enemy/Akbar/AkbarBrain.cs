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

        _runner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();

        if (Enemy.IsDead)
        {
            _runner.GetBlackboard().SetValue("TriggerExplosion", true);
            return;
        }

        player = GameManager.Instance.Player;
        if (player != null)
            _runner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _runner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        _runner.GetBlackboard().SetValue("SeePlayer", SeePlayer);
        _runner.GetBlackboard().SetValue("CanShoot", CanShootAtPlayer);
    }

    public void Explode()
    {
        if (Enemy.DistFromPlayer < _explosionRange)
            Enemy.Attack(player);

        Enemy.TakeDamage(Enemy.MaxHealth);
        _runner.GetBlackboard().SetValue("TriggerExplosion", true);
        _fxExplosion.Invoke();
        _explosionSound.Play();
    }
}