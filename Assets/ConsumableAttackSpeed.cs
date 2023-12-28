// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConsumableAttackSpeed : ConsumableChild, IConsumable
{
    [SerializeField] private float _attackSpeedAmount = 1f;

    public void Consume(Player player)
    {
        player.AttackSpeed += _attackSpeedAmount;
        Destroy(transform.root.gameObject);
    }
}