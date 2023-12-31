// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public class WaitNode : ActionNode
{
    [SerializeField] private string _duration ;
    private float _startTime;
    
    public override void OnStart()
    {
        _startTime = Time.time;
    }

    public override void OnStop()
    {

    }

    public override State OnUpdate()
    {
        if (!Blackboard.TryFind(_duration, out float duration))
            return State.Failure;
        
        return Time.time - _startTime > duration ? State.Success : State.Running;
    }
}