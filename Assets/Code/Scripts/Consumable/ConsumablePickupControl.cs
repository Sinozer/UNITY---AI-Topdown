// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConsumablePickupControl : ConsumableChild
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.root.TryGetComponent<Player>(out var player) == false)
            return;

        foreach (var consumable in Actions.GetComponentsInChildren<IConsumable>())
            consumable.Consume(player);
    }
}
