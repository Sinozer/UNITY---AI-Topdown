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

    public void Consume(Player player)
    {
        player.Heal(_healthAmount);
        Destroy(transform.root.gameObject);
    }
}