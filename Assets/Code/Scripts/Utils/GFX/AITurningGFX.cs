// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;

public class TurningGFX : MonoBehaviour
{
    private IAstarAI _ai;

    void Start()
    {
        _ai = GetComponentInParent<IAstarAI>();
    }

    void Update()
    {
        if (_ai == null)
            return;
        if (_ai.velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_ai.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}