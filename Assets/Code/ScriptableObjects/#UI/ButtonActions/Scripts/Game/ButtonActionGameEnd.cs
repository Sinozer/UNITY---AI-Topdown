// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionGameEnd", menuName = "ButtonAction/Game/End", order = 1)]
public class ButtonActionGameEnd : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        GameManager.Instance.End();
    }
}