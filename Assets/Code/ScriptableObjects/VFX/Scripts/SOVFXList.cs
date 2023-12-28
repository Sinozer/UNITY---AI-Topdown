// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 27/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New VFXList", menuName = "ScriptableObjects/VFX/SOVFXList")]
public class SOVFXList : SerializedScriptableObject
{
    public Dictionary<string, GameObject> List = new();

    /// <summary>
    /// Get a clone of the VFX prefab with the given name.
    /// </summary>
    /// <param name="name"> The name of the VFX prefab. </param>
    /// <returns> A clone of the VFX prefab with the given name. </returns>
    public GameObject GetVFX(string name)
    {
        if (List.TryGetValue(name, out GameObject vfx))
            return Instantiate(vfx);

        return null;
    }
}