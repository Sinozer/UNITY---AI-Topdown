// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionGamePlay", menuName = "ButtonAction/Game/Play", order = 0)]
public class ButtonActionGamePlay : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        GameManager.Instance.Play();
    }
}