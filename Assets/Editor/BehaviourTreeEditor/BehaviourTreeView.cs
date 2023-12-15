// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
   public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

   private BehaviourTree _tree;
   public BehaviourTreeView()
   {
      styleSheets.Add(
         AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor/BehaviorTreeEditor.uss"));
      SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
      this.AddManipulator(new ContentZoomer());
      this.AddManipulator(new ContentDragger());
      this.AddManipulator(new SelectionDragger());
      this.AddManipulator(new RectangleSelector());
      var grid = new GridBackground();
      Insert(0, grid);
      grid.StretchToParentSize();
   }

   public void PopulateView(BehaviourTree activeObject)
   {
      
   }
}