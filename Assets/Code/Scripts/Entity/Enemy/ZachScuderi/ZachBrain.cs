// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ZachBrain : EnemyBrain
{
    [SerializeField] EnemyBTRunner _runner;
    
    private float _elapsedTime;
    private float _meleeRange => Entity.AttackRange * 0.5f;
    private float _sprintCoolDown = 3.0f;
    private float _sprintElapsedTime;
    public bool CanMeleeAttack => Enemy.DistFromPlayer < _meleeRange;
    private bool _canSprint;
    protected override void Start()
    {
        base.Start();
        _runner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);
    }

    protected override void Update()
    {
        if (Entity.IsDead)
        {
            _runner.GetBlackboard().SetValue("IsDead", true);
            return;
        }
        
        base.Update();

        _sprintElapsedTime += Time.deltaTime;
        if (_elapsedTime >= _sprintCoolDown)
        {    _sprintElapsedTime = 0;
            _canSprint = true;
        }
        else
        {
            _canSprint = false; 
        }

        _runner.GetBlackboard().SetValue("CanSprint", _canSprint);
        
        Player player = GameManager.Instance.Player;

        _runner.GetBlackboard().SetValue("PlayerPosition", player != null ? (Vector2)player.transform.position : Vector2.zero);

        _elapsedTime += Time.deltaTime;
        
        _runner.GetBlackboard().SetValue("ElapsedTime", _elapsedTime);
        _runner.GetBlackboard().SetValue("SeePlayer", SeePlayer);
        _runner.GetBlackboard().SetValue("CanAttack", CanShootAtPlayer);
        _runner.GetBlackboard().SetValue("CanMeleeAttack", CanMeleeAttack);
    }
}
