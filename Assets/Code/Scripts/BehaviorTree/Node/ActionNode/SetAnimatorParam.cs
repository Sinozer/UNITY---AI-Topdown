// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class SetAnimatorParam : ActionNode
{
    public enum Type
    {
        BOOL,
        INT,
        FLOAT,
        TRIGGER
    }

    [EnumToggleButtons] public Type _type = Type.BOOL;

    [EnumToggleButtons] public EnemyBrain.AnimatorCondition _animatorCondition;

    [ShowIf("_type", Value = Type.INT)] public int IntCondition;

    [ShowIf("_type", Value = Type.FLOAT)] public float FloatCondition;

    [ShowIf("_type", Value = Type.BOOL)] public bool BoolCondition;


    private Animator _animator;
    private EnemyBrain _brain;
    private string[] _animatorConditionNames;

    public override void OnStart()
{
    if (!Blackboard.TryFind("EnemyBrain", out _brain))
    {
        return;
    }
    _animator = _brain.Animator;
    _animatorConditionNames = Enum.GetNames(typeof(EnemyBrain.AnimatorCondition));
}

    public override void OnStop()
    {
        
    }

    public override State OnUpdate()
{
    if (_animator == null || _brain == null)
        return State.Failure;

    SetAnimatorCondition(_animatorCondition);

    return State.Success;
}

private void SetAnimatorCondition(EnemyBrain.AnimatorCondition trueCondition)
{
    var trueConditionName = _animatorConditionNames[(int)trueCondition];

    AnimatorControllerParameter parameter = _animator.parameters.FirstOrDefault(p => p.name == trueConditionName);
    if (parameter == null)
    {
        Debug.LogError($"Animator does not have a parameter named {trueConditionName}");
        return;
    }

    switch (_type)
    {
        case Type.BOOL:
            if (parameter.type != AnimatorControllerParameterType.Bool)
            {
                Debug.LogError($"Animator parameter {trueConditionName} is not of type Bool");
                return;
            }
            _animator.SetBool(trueConditionName, BoolCondition);
            break;
        case Type.INT:
            if (parameter.type != AnimatorControllerParameterType.Int)
            {
                Debug.LogError($"Animator parameter {trueConditionName} is not of type Int");
                return;
            }
            _animator.SetInteger(trueConditionName, IntCondition);
            break;
        case Type.FLOAT:
            if (parameter.type != AnimatorControllerParameterType.Float)
            {
                Debug.LogError($"Animator parameter {trueConditionName} is not of type Float");
                return;
            }
            _animator.SetFloat(trueConditionName, FloatCondition);
            break;
        case Type.TRIGGER:
            if (parameter.type != AnimatorControllerParameterType.Trigger)
            {
                Debug.LogError($"Animator parameter {trueConditionName} is not of type Trigger");
                return;
            }
            _animator.SetTrigger(trueConditionName);
            break;
    }
}
}