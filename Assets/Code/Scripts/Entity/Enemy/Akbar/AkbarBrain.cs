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
    public UnityEvent FxExplosion => _fxExplosion;
    [SerializeField] private UnityEvent _fxExplosion;

    public AudioSource ExplosionSound => _explosionSound;
    [SerializeField] private AudioSource _explosionSound;

    public Player Player => GameManager.Instance.Player;
    public readonly float ExplosionRange = 3;

    protected override void Start()
    {
        base.Start();
        _btRunner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);
    }

    protected override void Update()
    {
        base.Update();

        if (Dead)
        {
            _btRunner.GetBlackboard().SetValue("TriggerExplosion", true);
            return;
        }

        if (Player != null)
            _btRunner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _btRunner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);

        _btRunner.GetBlackboard().SetValue("SeePlayer", IsInVisionRange);
        _btRunner.GetBlackboard().SetValue("CanShoot", IsInShootRange);
    }
}