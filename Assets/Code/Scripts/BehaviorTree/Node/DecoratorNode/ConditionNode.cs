// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConditionNode : DecoratorNode
{
    [SerializeField] private string _conditionName;

    public override void OnStart()
    {
    }

    public override void OnStop()
    {
    }

    public override State OnUpdate()
    {
        Blackboard.TryFind(_conditionName, out bool condition);
        if (condition)
        {
            Child.Update();
            return State.Success;
        }
        return State.Failure;
    }
}