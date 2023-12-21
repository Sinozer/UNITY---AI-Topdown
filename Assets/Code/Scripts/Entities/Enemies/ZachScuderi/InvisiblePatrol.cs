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
    private GameObject _parent;
    private SpriteRenderer _sprite;
    private float _elapsedTime;
    private float _fadeDurationScale => 0.5f;
    public override void OnStart()
    {
        Blackboard.TryFind("Self", out GameObject _self);
        _parent = _self.transform.parent.gameObject;
        _sprite = _parent.GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnStop()
    {
        FadeIn();
    }

    public override State OnUpdate()
    {
        if (_parent == null)
            return State.Failure;
        
        //FadeOut();
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