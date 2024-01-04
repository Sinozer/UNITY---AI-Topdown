// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 31/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using System.Collections;
using UnityEngine;

public class EntityLookAt : EntityAction, IActionTargetable
{
    public Transform Target { get; set; }

    private IAstarAI _aiBrain;

    private void Awake()
    {
        _aiBrain = Brain.GetComponentInChildren<IAstarAI>(true);
    }

    private void OnEnable()
    {
        Execute();
    }

    private void OnDisable()
    {
        Stop();
    }

    protected override IEnumerator Action()
    {
        while (true)
        {
            Vector3 direction = Vector2.zero;

            if (Target == null)
            {
                if (_aiBrain != null && _aiBrain.hasPath != false)
                    direction = _aiBrain.velocity;

                yield return null;
            }
            else
            {
                direction = Target.position - transform.position;

                yield return null;
            }

            int degree = direction.x > 0 ? 0 : 180;

            Render.transform.rotation = Quaternion.Euler(0, degree, 0);
            Physics.transform.rotation = Quaternion.Euler(0, degree, 0);

            if (VFX != null)
                VFX.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            yield return null;
        }
    }
}