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

[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable/New", order = 0)]
public class SOLootTable : SerializedScriptableObject
{
    [Serializable]
    public class LootType
    {
        /// <summary>
        /// The item to drop.
        /// </summary>
        public GameObject Item => _item;
        [SerializeField] private GameObject _item;

        /// <summary>
        /// The chance to drop the item (0-1).
        /// </summary>
        public float DropChance
        {
            get => _dropChance;
            set => _dropChance = Mathf.Clamp(value, 0, 1);
        }
        [SerializeField, MinValue(0f), MaxValue(1f)] protected float _dropChance;

        /// <summary>
        /// The number of items that can be dropped.
        /// </summary>
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
                if (loot.DropChance < UnityEngine.Random.Range(0f, 1f))
                    continue;
                
                Vector3 spawnPosition = parent.transform.position;
                spawnPosition.x += UnityEngine.Random.Range(-1f, 1f);
                spawnPosition.y += UnityEngine.Random.Range(-1f, 1f);

                Instantiate(loot.Item, spawnPosition, Quaternion.identity);
            }
        }
    }
}