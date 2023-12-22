// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;

public class ZachBrain : EnemyBrain
{
    [SerializeField] EnemyBTRunner _runner;
    
    private float _elapsedTime;
    private float _fadeTime = 0.2f;
    private float _meleeRange => _entity.AttackRange * 0.5f;
    private float _sprintCoolDown = 3.0f;
    private float _sprintElapsedTime;
    private bool _canMeleeAttack;
    private bool _canSprint;
    protected override void Start()
    {
        base.Start();
        _runner.GetBlackboard().SetValue("AttackSpeed", _entity.AttackSpeed);
    }

    protected override void Update()
    {
        if (_entity.IsDead)
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
        
        _canShootAtPlayer = _enemy.DistFromPlayer < _entity.AttackRange;
        _canMeleeAttack = _enemy.DistFromPlayer < _meleeRange;
        _seePlayer = _enemy.DistFromPlayer < _entity.VisionRange;
        
        Player player = GameManager.Instance.Player;
        if (player != null)
            _runner.GetBlackboard().SetValue("PlayerPosition", (Vector2)GameManager.Instance.Player.transform.position);
        else
            _runner.GetBlackboard().SetValue("PlayerPosition", Vector2.zero);
        
        _elapsedTime += Time.deltaTime;
        
        _runner.GetBlackboard().SetValue("ElapsedTime", _elapsedTime);
        _runner.GetBlackboard().SetValue("SeePlayer", _seePlayer);
        _runner.GetBlackboard().SetValue("CanAttack", _canShootAtPlayer);
        _runner.GetBlackboard().SetValue("CanMeleeAttack", _canMeleeAttack);
    }
}
