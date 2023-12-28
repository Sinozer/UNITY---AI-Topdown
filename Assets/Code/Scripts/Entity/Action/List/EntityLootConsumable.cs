// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class EntityLootConsumable : EntityChild, IEntityAction
{
    private void OnEnable()
    {
        Entity.OnDeath += Loot;
    }

    private void OnDisable()
    {
        Entity.OnDeath -= Loot;
    }

    #region Fields
    public List<SOLootTable> LootTables => _lootTables;
    [SerializeField, InlineEditor] private List<SOLootTable> _lootTables;
    #endregion Fields

    #region Methods
    public void Loot()
    {
        foreach (var lt in _lootTables)
        {
            lt.Loot(this);
        }
    }
    #endregion Methods

    public void ResetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void SetAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }
}