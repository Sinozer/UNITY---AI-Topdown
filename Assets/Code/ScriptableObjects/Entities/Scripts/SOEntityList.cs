// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;

public class SOEntityList : SerializedScriptableObject
{
    public Dictionary<string, SOEntity> List = new();

#if UNITY_EDITOR

    private void Awake()
    {
        List.Clear();
        var guids = UnityEditor.AssetDatabase.FindAssets("t:SOEntity", new[] { "Assets/Code/ScriptableObjects/Entities/List" });
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var entity = UnityEditor.AssetDatabase.LoadAssetAtPath<SOEntity>(path);
            List.Add(entity.name, entity);
        }
    }

    [Button("Add New Entity Data")]
    public void AddEntity()
    {
        var newEntity = CreateInstance<SOEntity>();
        newEntity.name = "NewEntityData";

        var path = $"Assets/Code/ScriptableObjects/Entities/List/{newEntity.name}.asset";
        var i = 1;
        while (System.IO.File.Exists(path))
        {
            path = $"Assets/Code/ScriptableObjects/Entities/List/{newEntity.name}({i}).asset";
            i++;
        }
        UnityEditor.AssetDatabase.CreateAsset(newEntity, path);

        List.Add(newEntity.name, newEntity);

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif
}