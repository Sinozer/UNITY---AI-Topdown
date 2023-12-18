// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ExampleNode : ActionNode
{
    GameObject _self;

    public override void OnStart()
    {
        Blackboard.TryFind("Self", out _self);
    }

    public override void OnStop()
    {
    }

    public override State OnUpdate()
    {
        if (_self == null)
            return State.Failure;

        if (Blackboard.TryFind("ok", out float ok))
        {
            Debug.Log("ok: " + ok);
            Blackboard.SetValue("ok", ok + 1);
        }

        if (_self.transform.position.x < 10.0f)
        {
            _self.transform.position += Vector3.right * Time.deltaTime;
            return State.Running;
        }

        return State.Success;
    }
}