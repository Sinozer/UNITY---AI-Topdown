using UnityEngine;
using UnityEditor;

public class KeywordReplace : UnityEditor.AssetModificationProcessor
{
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0)
            return;

        string file = path.Substring(index);
        if (file != ".cs" && file != ".hlsl")
            return;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        if (!System.IO.File.Exists(path))
            return;

        string fileContent = System.IO.File.ReadAllText(path);

        fileContent = fileContent.Replace("#DATETIME#", System.DateTime.Now.ToString("dd/MM/yy"));
        fileContent = fileContent.Replace("#PROJECTNAME#", PlayerSettings.productName);
        fileContent = fileContent.Replace("#DEVELOPERNAME#", System.Environment.UserName);

        System.IO.File.WriteAllText(path, fileContent);
        AssetDatabase.Refresh();
    }
}