// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ButtonReferencing : MonoBehaviour
{
    public ButtonAction Action;

    public UnityEngine.UI.Button Button
    {
        get
        {
            if (_button == null)
                _button = GetComponent<UnityEngine.UI.Button>();
            return _button;
        }
    }
    private UnityEngine.UI.Button _button;

    public ButtonActionData Data;

    private void Awake()
    {
        if (Action == null)
            return;

        Button.onClick.AddListener(() => Action.Execute(Data));
    }
}