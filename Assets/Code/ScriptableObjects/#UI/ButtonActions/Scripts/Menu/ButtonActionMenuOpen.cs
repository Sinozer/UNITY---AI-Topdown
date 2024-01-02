// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 02/01/24
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionMenuOpen", menuName = "ButtonAction/Menu/Open")]
public class ButtonActionMenuOpen : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        if (data.StringData == default || MenuManager.IsInitialized == false)
        {
            Debug.LogError("No menu name provided or MenuManager not initialized");
            return;
        }

        MenuManager.Instance.OpenMenu(data.StringData);
    }
}