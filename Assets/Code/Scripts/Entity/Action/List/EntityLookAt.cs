// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 31/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System.Collections;
using UnityEngine;

public class EntityLookAt : EntityChild, IEntityAction
{
    public Transform Target { get; set; }

    private IAstarAI _aiBrain;

    public void LookAtTarget()
    {
        if (Target == null)
        {
            if (_aiBrain == null)
                return;

            if (_aiBrain.hasPath == false)
                return;

            var deg = _aiBrain.velocity.x > 0 ? 0 : 180;

            Render.transform.rotation = Quaternion.Euler(0, deg, 0);
            Physics.transform.rotation = Quaternion.Euler(0, deg, 0);

            return;
        }

        var direction = Target.position - transform.position;

        int degree = direction.x > 0 ? 0 : 180;

        Render.transform.rotation = Quaternion.Euler(0, degree, 0);
        Physics.transform.rotation = Quaternion.Euler(0, degree, 0);

        if (VFX != null)
            VFX.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void Awake()
    {
        _aiBrain = Brain.GetComponentInChildren<IAstarAI>(true);
    }

    public void Update()
    {
        LookAtTarget();
    }

    public void ResetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void SetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }
}