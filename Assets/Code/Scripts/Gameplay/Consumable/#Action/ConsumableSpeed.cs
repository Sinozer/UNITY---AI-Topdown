// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConsumableSpeed : ConsumableChild, IConsumable
{
    [SerializeField] private float _speedAmount = 5f;

    public void Consume(Entity entity)
    {
        entity.Data.TryFind<float>("MovementSpeed", out float movementSpeed);

        entity.Data.SetValue("MovementSpeed", movementSpeed + _speedAmount);

        Destroy(transform.root.gameObject);
    }
}