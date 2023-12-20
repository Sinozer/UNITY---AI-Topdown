// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public class ConditionNode : ActionNode
{
    public enum Type
    {
        BOOL,
        INT,
        FLOAT,
        STRING
    }
    [EnumToggleButtons] public Type _type = Type.BOOL;

    public enum Operator
    {
        EQUAL,
        NOT_EQUAL,
        GREATER,
        LESS,
        GREATER_EQUAL,
        LESS_EQUAL
    }

    [SerializeField] private string _conditionName;
    [SerializeField] private bool _not;

    [ShowIf("@_type == Type.INT || _type == Type.FLOAT"), EnumToggleButtons]
    public Operator CalcOperator;

    [ShowIf("_type", Value = Type.INT)]
    public int IntCondition;

    [ShowIf("_type", Value = Type.FLOAT)] 
    public float FloatCondition;

    [ShowIf("_type", Value = Type.STRING)]
    public string StringCondition;

    public override void OnStart()
    {
    }

    public override void OnStop()
    {
    }

    public override State OnUpdate()
    {
        switch (_type)
        {
            case Type.BOOL:
                return CheckBool();
            case Type.INT:
                return CheckInt();
            case Type.FLOAT:
                return CheckFloat();
            case Type.STRING:
                return CheckString();
            default:
                return State.Failure;
        }
    }

    private State CheckString()
    {
        if (!Blackboard.TryFind(_conditionName, out string condition))
            return State.Failure;

        if (condition == StringCondition ^ _not)
        {
            return State.Success;
        }

        return State.Failure;
    }

    private State CheckFloat()
    {
        if (!Blackboard.TryFind(_conditionName, out float condition))
            return State.Failure;

        switch (CalcOperator)
        {
            case Operator.EQUAL:
                if (condition == FloatCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.NOT_EQUAL:
                if (condition != FloatCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.GREATER:
                if (condition > FloatCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.LESS:
                if (condition < FloatCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.GREATER_EQUAL:
                if (condition >= FloatCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.LESS_EQUAL:
                if (condition <= FloatCondition ^ _not)
                {
                    return State.Success;
                }
                break;
        }

        return State.Failure;
    }

    private State CheckInt()
    {
        if (!Blackboard.TryFind(_conditionName, out int condition))
            return State.Failure;

        switch (CalcOperator)
        {
            case Operator.EQUAL:
                if (condition == IntCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.NOT_EQUAL:
                if (condition != IntCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.GREATER:
                if (condition > IntCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.LESS:
                if (condition < IntCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.GREATER_EQUAL:
                if (condition >= IntCondition ^ _not)
                {
                    return State.Success;
                }
                break;
            case Operator.LESS_EQUAL:
                if (condition <= IntCondition ^ _not)
                {
                    return State.Success;
                }
                break;
        }

        return State.Failure;
    }

    private State CheckBool()
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