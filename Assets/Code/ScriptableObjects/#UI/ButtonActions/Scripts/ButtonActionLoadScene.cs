// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonActionLoadScene", menuName = "ButtonAction/LoadScene")]
public class ButtonActionLoadScene : ButtonAction
{
    public override void Execute(ButtonActionData data = default)
    {
        if (data.IntData != default)
        {
            SceneManager.Instance.LoadScene(data.IntData);
            return;
        }

        if (data.StringData != default)
        {
            SceneManager.Instance.LoadScene(data.StringData);
            return;
        }

        Debug.LogError("No scene name or index provided");
    }
}