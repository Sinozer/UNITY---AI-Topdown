// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConditionNode : ActionNode
{
    [SerializeField] private string _conditionName;
    [SerializeField] private bool _not;

    public override void OnStart()
    {
    }

    public override void OnStop()
    {
    }

    public override State OnUpdate()
    {
        if (!Blackboard.TryFind(_conditionName, out bool condition))
            return State.Failure;

        if (condition ^ _not)
        {
            return State.Success;
        }

        return State.Failure;
    }
}