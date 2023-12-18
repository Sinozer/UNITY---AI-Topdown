// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
   public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

   private Editor _editor;
   
   public InspectorView()
   {
      
   }

   internal void UpdateSelection(NodeView nodeView)
   {
      Clear();
      
      Object.DestroyImmediate(_editor);
      _editor = Editor.CreateEditor(nodeView.Node);
      IMGUIContainer container = new IMGUIContainer(() => 
      {
          if (_editor.target)
              _editor.OnInspectorGUI();
      });
      Add(container);
   }
}