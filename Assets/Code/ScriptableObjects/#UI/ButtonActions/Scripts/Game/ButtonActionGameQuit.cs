// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionGameQuit", menuName = "ButtonAction/Game/Quit")]
public class ButtonActionGameQuit : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        GameManager.Instance.Quit();
    }
}