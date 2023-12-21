// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;

public class InvisiblePatrol : ActionNode
{
    private GameObject _self;
    private SpriteRenderer _sprite;
    private ZachBrain _brain;
    private float _elapsedTime;
    private float _fadeDurationScale => 0.5f;
    
    public override void OnStart()
    {
        if (!Blackboard.TryFind("Self", out GameObject _self))
            return;
        if (!Blackboard.TryFind("EnemyBrain", out EnemyBrain _brain))
            return;
        
        _sprite = _self.GetComponentInChildren<SpriteRenderer>();
        
        _brain.Patrolling(true);
        _brain.AIPath(true);
    }

    public override void OnStop()
    {
        FadeIn();
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;
        Blackboard.TryFind("SeePlayer", out bool _seePlayer);
        if (_seePlayer)
        {
            return State.Success;
        }
        
        FadeOut();
        
        return State.Running;
    }

    private void FadeOut()
    {
        Color color = _sprite.color;
        _elapsedTime += Time.deltaTime * _fadeDurationScale; 
        float alpha = Mathf.Lerp(1, 0, _elapsedTime);
        color.a = alpha;
        _sprite.color = color;
    }
    
    private void FadeIn()
    {
        Color color = _sprite.color;
        _elapsedTime += Time.deltaTime * _fadeDurationScale; 
        float alpha = Mathf.Lerp(0, 1, _elapsedTime);
        color.a = alpha;
        _sprite.color = color;
    }
}