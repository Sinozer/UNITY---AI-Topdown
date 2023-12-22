// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibleNode : ActionNode
{
    private GameObject _self;
    private SpriteRenderer _sprite;
    private EnemyBrain _brain;

    public override void OnStart()
    {
        if (!Blackboard.TryFind("Self", out  _self))
            return;
        
        _sprite = _self.GetComponentInChildren<SpriteRenderer>();
        Blackboard.TryFind("EnemyBrain", out _brain);
    }

    public override void OnStop()
    {
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;

        FadeOut();
        return State.Success;
    }

    private void FadeOut()
    {
        Color color = _sprite.color;
        Blackboard.TryFind("ElapsedTime", out float _elapsedTime);
        color.a = Mathf.Lerp(1, 0, _brain.DistFromPlayer - 5);
        _sprite.color = color;
    }
    
}