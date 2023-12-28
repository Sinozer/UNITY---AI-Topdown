// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 25/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioClipList", menuName = "ScriptableObjects/AudioClips/SOAudioClipList")]
public class SOAudioClipList : SerializedScriptableObject
{
    public Dictionary<string, AudioClip> List = new();

    /// <summary>
    /// Get the audio clip with the given name.
    /// </summary>
    /// <param name="name"> The name of the audio clip. </param>
    /// <returns> The audio clip with the given name. </returns>
    public AudioClip GetAudioClip(string name)
    {
        if (List.TryGetValue(name, out AudioClip clip))
            return clip;

        return null;
    }

#if UNITY_EDITOR
    [Button("Refresh From Files", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void RefreshFromFiles()
    {
        List.Clear();
        var guids = UnityEditor.AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Resources/Audio/SFX" });
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var audioClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            List.Add(audioClip.name, audioClip);
        }
    }
#endif
}