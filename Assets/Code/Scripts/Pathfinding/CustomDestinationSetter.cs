// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Pathfinding;
using UnityEngine;

public class CustomDestinationSetter : MonoBehaviour
{
    private Transform _target;
    IAstarAI _ai;

    private void OnEnable()
    {
        _ai = GetComponent<IAstarAI>();

        if (_ai != null) 
            _ai.onSearchPath += Update;
    }

    private void OnDisable()
    {
        if (_ai != null) 
            _ai.onSearchPath -= Update;
    }

    private void Start()
    {
        _target = GameManager.Instance.GetPlayer().transform;
        if (_target == null)
            _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_target != null && _ai != null) 
            _ai.destination = _target.position;
    }
}