// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Consumable : ConsumableChild
{
    [SerializeField] private float _yOffset = 0.1f;

    float _timeWhenDropped;

    private void Start()
    {
        _timeWhenDropped = Time.time;
    }

    private void Update()
    {
        transform.position += _yOffset * Mathf.Sin((Time.time - _timeWhenDropped) * 2) * Time.deltaTime * Vector3.up;
    }
}