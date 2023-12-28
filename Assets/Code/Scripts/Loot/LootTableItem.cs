// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public class LootTableItem : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// The chance to drop the item (0-1).
    /// </summary>
    public float DropChance
    {
        get => _dropChance;
        set => _dropChance = Mathf.Clamp(value, 0, 1);
    }
    [SerializeField, MinValue(0f), MaxValue(1f)] protected float _dropChance;
    #endregion Fields
}