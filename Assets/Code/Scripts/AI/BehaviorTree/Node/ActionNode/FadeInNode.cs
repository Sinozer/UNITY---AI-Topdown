// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class FadeInNode : ActionNode
{
    private GameObject _self;
    private SpriteRenderer _sprite;
    
    public override void OnStart()
    {
        if (!Blackboard.TryFind("Self", out  _self))
            return;
        
        _sprite = _self.GetComponentInChildren<SpriteRenderer>();

    }

    public override void OnStop()
    {
       
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;
        
        FadeIn();
        return State.Success;
    }
    
    
    private void FadeIn()
    {
        Color color = _sprite.color;
        Blackboard.TryFind("ElapsedTime", out float _elapsedTime);
        color.a = Mathf.Lerp(0, 1, _elapsedTime);
        _sprite.color = color;
    }
}