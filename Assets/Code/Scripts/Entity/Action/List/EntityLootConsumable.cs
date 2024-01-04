// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLootConsumable : EntityAction
{
    /// <summary>
    /// Loot tables to use when looting.
    /// </summary>
    public List<SOLootTable> LootTables => _lootTables;
    [SerializeField, InlineEditor] private List<SOLootTable> _lootTables;

    protected override IEnumerator Action()
    {
        foreach (var lootTable in _lootTables)
        {
            lootTable.Loot(this);
        }

        yield return null;
    }

    private void OnEnable()
    {
        Entity.OnDeath += Execute;
    }

    private void OnDisable()
    {
        Entity.OnDeath -= Execute;
    }
}