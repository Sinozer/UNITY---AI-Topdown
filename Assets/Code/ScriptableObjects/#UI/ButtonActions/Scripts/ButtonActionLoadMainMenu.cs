// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionLoadMainMenu", menuName = "ButtonAction/LoadMainMenu")]
public class ButtonActionLoadMainMenu : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        SceneManager.Instance.LoadScene(0);
    }
}