// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ConsumableHeal : ConsumableChild, IConsumable
{
    [SerializeField] private float _yOffset = 0.005f;
    [SerializeField] private int _healthAmount = 10;

    public void Consume(Player player)
    {
        player.Heal(_healthAmount);
        Destroy(transform.root.gameObject);
    }

    private void Update()
    {
        Consumable.transform.position += _yOffset * Mathf.Sin(Time.time * 2) * Time.deltaTime * Vector3.up;
    }
}