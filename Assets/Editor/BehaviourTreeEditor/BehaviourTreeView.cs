// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits>
    {
    }
    
    public Action<NodeView> OnNodeSelected;

    private BehaviourTree _tree;

    
    public BehaviourTreeView()
    {
        styleSheets.Add(
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor/BehaviourTreeEditor.uss"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }

    NodeView FindNodeView(Node node)
    {
        return graphElements.ToList().Find(view => (view as NodeView).Node == node) as NodeView;
    }
    
    internal void PopulateView(BehaviourTree tree)
    {
        _tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (_tree.RootNode == null)
        {
            _tree.RootNode = _tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(_tree);
            AssetDatabase.SaveAssets();
        }

        // Create nodes views
        tree.Nodes.ForEach(CreateNodeView);
        
        // Create edges views
        tree.Nodes.ForEach(n =>
        {
            List<Node> children = tree.GetChildren(n);
            children.ForEach(c =>
            {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);
                Edge edge = parentView.OutputPort.ConnectTo(childView.InputPort);
                AddElement(edge);
            });
        });
        
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        if (graphviewchange.elementsToRemove != null)
        {
            graphviewchange.elementsToRemove.ForEach(element =>
            {
                switch (element)
                {
                    case NodeView nodeView:
                        _tree.DeleteNode(nodeView.Node);
                        break;
                    case Edge edge:
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        _tree.RemoveChild(parentView.Node, childView.Node);
                        break;
                    }
                }
            });
        }
        if (graphviewchange.edgesToCreate != null)
            graphviewchange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _tree.AddChild(parentView.Node, childView.Node);
            });

        return graphviewchange;

    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Add Node/{type.BaseType.Name}/{type.Name}",
                    a => CreateNode(type));
            }
        }

        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Add Node/{type.BaseType.Name}/{type.Name}",
                    a => CreateNode(type));
            }
        }

        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Add Node/{type.BaseType.Name}/{type.Name}",
                    a => CreateNode(type));
            }
        }
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(port =>
            port.direction != startPort.direction &&
            port.node != startPort.node).ToList();
    }

    private void CreateNode(Type type)
    {
        Node node = _tree.CreateNode(type);
        CreateNodeView(node);
    }


    private void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }
}