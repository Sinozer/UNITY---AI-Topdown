// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ZachBrain : EnemyBrain
{
    private float _elapsedTime;
    private float MeleeRange => Entity.AttackRange * 0.5f;
    private readonly float _sprintCoolDown = 3.0f;
    public bool CanMeleeAttack => Enemy.DistFromPlayer < MeleeRange;
    private bool _canSprint;
    protected override void Start()
    {
        base.Start();
        _btRunner.GetBlackboard().SetValue("AttackSpeed", Entity.AttackSpeed);
    }

    protected override void Update()
    {
        if (Entity.IsDead)
        {
            _btRunner.GetBlackboard().SetValue("IsDead", true);
            return;
        }
        
        base.Update();

        if (_elapsedTime >= _sprintCoolDown)
        {    
            _canSprint = true;
        }
        else
        {
            _canSprint = false; 
        }

        _btRunner.GetBlackboard().SetValue("CanSprint", _canSprint);
        
        Player player = GameManager.Instance.Player;

        _btRunner.GetBlackboard().SetValue("PlayerPosition", player != null ? (Vector2)player.transform.position : Vector2.zero);

        _elapsedTime += Time.deltaTime;
        
        _btRunner.GetBlackboard().SetValue("ElapsedTime", _elapsedTime);
        _btRunner.GetBlackboard().SetValue("SeePlayer", SeePlayer);
        _btRunner.GetBlackboard().SetValue("CanAttack", CanShootAtPlayer);
        _btRunner.GetBlackboard().SetValue("CanMeleeAttack", CanMeleeAttack);
    }
}
