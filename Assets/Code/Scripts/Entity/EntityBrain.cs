// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Linq;
using UnityEngine;

public class EntityBrain : EntityChild
{
    public enum AnimatorCondition
    {
        Idle,
        IdleActivated,
        Activating,
        Run,
        Attack,
        Hit,
        Dead
    }

    // Take in parameter the value (string, int, float, bool)
    public bool SetAnimatorCondition(AnimatorCondition condition, object value = null)
    {
        AnimatorControllerParameter parameter = Animator.parameters.FirstOrDefault(p => p.name == condition.ToString());

        if (parameter == null)
            return false;

        switch (parameter.type)
        {
            case AnimatorControllerParameterType.Bool:
                if (value is not bool)
                    return false;

                Animator.SetBool(condition.ToString(), (bool)value);
                break;
            case AnimatorControllerParameterType.Float:
                if (value is not float)
                    return false;

                Animator.SetFloat(condition.ToString(), (float)value);
                break;
            case AnimatorControllerParameterType.Int:
                if (value is not int)
                    return false;

                Animator.SetInteger(condition.ToString(), (int)value);
                break;
            case AnimatorControllerParameterType.Trigger:
                Animator.SetTrigger(condition.ToString());
                break;
        }


        return true;
    }
}