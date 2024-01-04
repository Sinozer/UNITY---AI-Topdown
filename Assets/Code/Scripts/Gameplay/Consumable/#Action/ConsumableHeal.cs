// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConsumableHeal : ConsumableChild, IConsumable
{
    [SerializeField] private int _healthAmount = 10;

    public void Consume(Entity entity)
    {
        entity.Heal(_healthAmount);
        Destroy(transform.root.gameObject);
    }
}