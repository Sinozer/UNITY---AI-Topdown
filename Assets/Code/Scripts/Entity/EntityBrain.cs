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

    public EntityMove MovementAction
    {
        get
        {
            if (_movementAction == null)
                _movementAction = GetAction<EntityMove>();
            return _movementAction;
        }
    }
    private EntityMove _movementAction;

    public EntityShoot ShootAction
    {
        get
        {
            if (_shootingAction == null)
                _shootingAction = GetAction<EntityShoot>();

            return _shootingAction;
        }
    }
    private EntityShoot _shootingAction;

    public EntityDash DashingAction
    {
        get
        {
            if (_dashingAction == null)
                _dashingAction = GetAction<EntityDash>();

            return _dashingAction;
        }
    }
    private EntityDash _dashingAction;
}