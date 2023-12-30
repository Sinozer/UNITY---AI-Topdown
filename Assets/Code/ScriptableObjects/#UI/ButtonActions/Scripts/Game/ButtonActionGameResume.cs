// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionGameResume", menuName = "ButtonAction/Game/Resume", order = 1)]
public class ButtonActionGameResume : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        GameManager.Instance.Resume();
    }
}