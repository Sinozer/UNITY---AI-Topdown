// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;

public class SOProjectileList : SerializedScriptableObject
{
    public Dictionary<string, SOProjectile> List = new();

#if UNITY_EDITOR

    [HorizontalGroup("Split", 0.5f)]
    [Button("Add Projectile", ButtonSizes.Large), GUIColor(0, 1, 0)]
    public void AddEntity()
    {
        var newProjectile = CreateInstance<SOProjectile>();
        newProjectile.name = "NewProjectile";

        var path = $"Assets/Code/ScriptableObjects/Projectiles/List/{newProjectile.name}.asset";
        var i = 1;
        while (System.IO.File.Exists(path))
        {
            path = $"Assets/Code/ScriptableObjects/Projectiles/List/{newProjectile.name}({i}).asset";
            i++;
        }
        UnityEditor.AssetDatabase.CreateAsset(newProjectile, path);

        List.Add(newProjectile.name, newProjectile);

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }
 
    [VerticalGroup("Split/right")]
    [Button("Refresh From Files", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void RefreshFromFiles()
    {
        List.Clear();
        var guids = UnityEditor.AssetDatabase.FindAssets("t:SOProjectile", new[] { "Assets/Code/ScriptableObjects/Projectiles/List" });
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var projectile = UnityEditor.AssetDatabase.LoadAssetAtPath<SOProjectile>(path);
            List.Add(projectile.name, projectile);
        }
    }
#endif
}