// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 02/01/24
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionMenuClose", menuName = "ButtonAction/Menu/Close")]
public class ButtonActionMenuClose : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        if (MenuManager.IsInitialized == false)
        {
            Debug.LogError("MenuManager not initialized");
            return;
        }

        MenuManager.Instance.CloseMenu();
    }
}