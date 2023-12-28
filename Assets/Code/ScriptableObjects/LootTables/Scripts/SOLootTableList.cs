// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTableList", menuName = "ScriptableObjects/LootTable/List", order = 1)]
public class SOLootTableList : SerializedScriptableObject
{
    public Dictionary<string, SOLootTable> List = new();

#if UNITY_EDITOR

    [HorizontalGroup("Split", 0.5f)]
    [Button("Add LootTable", ButtonSizes.Large), GUIColor(0, 1, 0)]
    public void AddEntity()
    {
        var newLootTable = CreateInstance<SOLootTable>();
        newLootTable.name = "NewLootTable";

        var path = $"Assets/Code/ScriptableObjects/LootTables/List/{newLootTable.name}.asset";
        var i = 1;
        while (System.IO.File.Exists(path))
        {
            path = $"Assets/Code/ScriptableObjects/LootTables/List/{newLootTable.name}({i}).asset";
            i++;
        }
        UnityEditor.AssetDatabase.CreateAsset(newLootTable, path);

        List.Add(newLootTable.name, newLootTable);

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }

    [VerticalGroup("Split/right")]
    [Button("Refresh From Files", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void RefreshFromFiles()
    {
        List.Clear();
        var guids = UnityEditor.AssetDatabase.FindAssets("t:SOLootTable", new[] { "Assets/Code/ScriptableObjects/LootTables/List" });
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var LootTable = UnityEditor.AssetDatabase.LoadAssetAtPath<SOLootTable>(path);
            List.Add(LootTable.name, LootTable);
        }
    }
#endif
}