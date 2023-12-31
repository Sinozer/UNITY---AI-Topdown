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

    public void Consume(Player player)
    {
        player.MovementSpeed += _speedAmount;
        Destroy(transform.root.gameObject);
    }
}