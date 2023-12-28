// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable", order = 1)]
public class SOLootTable : SerializedScriptableObject
{
    [Serializable]
    public class LootType
    {
        public LootTableItem Item => _item;
        [SerializeField, InlineEditor] private LootTableItem _item;

        public int NumberOfItems => _numberOfItems;
        [SerializeField] private int _numberOfItems;
    }

    public List<LootType> Loots => _loots;
    [SerializeField] private List<LootType> _loots;

    public void Loot(EntityLootConsumable parent)
    {
        foreach (var loot in Loots)
        {
            for (int i = 0; i < loot.NumberOfItems; i++)
            {
                if (loot.Item.DropChance < UnityEngine.Random.Range(0f, 1f))
                    continue;
                
                Vector3 spawnPosition = parent.transform.position;
                spawnPosition.x += UnityEngine.Random.Range(-1f, 1f);
                spawnPosition.y += UnityEngine.Random.Range(-1f, 1f);

                Instantiate(loot.Item, spawnPosition, Quaternion.identity);
            }
        }
    }
}