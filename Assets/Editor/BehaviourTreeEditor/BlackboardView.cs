// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackboardView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BlackboardView, UxmlTraits> { }

    private CustomBlackboard _blackboard;
    private Editor _editor;

    public BlackboardView()
    {
    }

    public void InspectBlackboard(CustomBlackboard blackboard)
    {
        _blackboard = blackboard;
        Clear();
        Object.DestroyImmediate(_editor);
        _editor = Editor.CreateEditor(_blackboard);
        IMGUIContainer container = new IMGUIContainer(() => { _editor.OnInspectorGUI(); });
        Add(container);
    }
}