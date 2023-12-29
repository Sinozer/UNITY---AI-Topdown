// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionQuit", menuName = "ButtonAction/Quit")]
public class ButtonActionQuit : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        Application.Quit();
    }
}