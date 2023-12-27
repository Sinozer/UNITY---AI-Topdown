// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 27/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public class VFXManager : EntityChild
{
    [Required, SerializeField, InlineEditor] private SOVFXList _vfxList;

    public GameObject PlayVFX(string name)
    {
        GameObject vfx = _vfxList.GetVFX(name);
        if (vfx == null)
        {
            Debug.LogError("VFX not found: " + name);
            return null;
        }

        //vfx.transform.SetParent(Externals.transform);
        vfx.transform.position = transform.position;
        vfx.SetActive(true);

        return vfx;
    }

    public void StopVFX(GameObject vfx)
    {
        vfx.SetActive(false);

        Destroy(vfx);
    }
}