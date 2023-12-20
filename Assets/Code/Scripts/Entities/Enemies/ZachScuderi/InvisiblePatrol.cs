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
    private float _elapsedTime;
    private float _fadeDuration => 2.0f;
    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
        _sprite = _self.GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;
        
        //FadeOut();
        return State.Running;
    }

    // private void FadeOut()
    // {
    //     Color color = _sprite.color;
    //     _elapsedTime += Time.deltaTime; 
    //     float alpha = Mathf.Lerp(1, 0, _elapsedTime);
    //     color.a = alpha;
    //     _sprite.color = color;
    // }
}