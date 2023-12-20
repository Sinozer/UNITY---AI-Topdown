// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits>
    {
    }
    
    public Action<NodeView> OnNodeSelected;

    private static BehaviourTree _tree;

    
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

        Undo.undoRedoPerformed += OnUndoRedoPerformed;
        //serializeGraphElements += OnSerializeGraphElements;
        //unserializeAndPaste += OnUnserializeAndPaste;
    }

    //private void OnUnserializeAndPaste(string operationName, string data)
    //{
    //    if (operationName == "Paste")
    //    {
    //        string[] lines = data.Split('\n');
    //        List<Node> nodes = new List<Node>();
    //        List<Edge> edges = new List<Edge>();
    //        foreach (string line in lines)
    //        {
    //            if (string.IsNullOrEmpty(line))
    //                continue;
    //            string[] elements = line.Split(' ');
    //            if (elements.Length == 3)
    //            {
    //                Node node = _tree.CreateNode(Type.GetType(elements[0]));
    //                node.Position = new Vector2(float.Parse(elements[1]), float.Parse(elements[2]));
    //                nodes.Add(node);
    //            }
    //            else if (elements.Length == 2)
    //            {
    //                Node parent = nodes.Find(n => n.Guid == elements[0]);
    //                Node child = nodes.Find(n => n.Guid == elements[1]);
    //                edges.Add(FindNodeView(parent).OutputPort.ConnectTo(FindNodeView(child).InputPort));
    //            }
    //        }

    //        nodes.ForEach(CreateNodeView);
    //        edges.ForEach(AddElement);
    //    }
    //}

    //private string OnSerializeGraphElements(IEnumerable<GraphElement> elements)
    //{
    //    string data = string.Empty;
    //    elements.ToList().ForEach(element =>
    //    {
    //        switch (element)
    //        {
    //            case NodeView nodeView:
    //                data += $"{nodeView.Node.name} {nodeView.Node.Guid} {nodeView.GetPosition().position.x} {nodeView.GetPosition().position.y}\n";
    //                break;
    //            case Edge edge:
    //                NodeView parentView = edge.output.node as NodeView;
    //                NodeView childView = edge.input.node as NodeView;
    //                data += $"{parentView.Node.Guid} {childView.Node.Guid}\n";
    //                break;
    //        }
    //    });
    //    return data;
    //}

    private void OnUndoRedoPerformed()
    {
        PopulateView(_tree);
    }

    NodeView FindNodeView(Node node)
    {
        return graphElements.ToList().Find(view => (view as NodeView).Node == node) as NodeView;
    }

    public void UpdateTreeGUI()
    {
        nodes.ForEach(node =>
        {
            NodeView nodeView = node as NodeView;
            nodeView.UpdateNodeGUI();
        });
    }
    
    internal void PopulateView(BehaviourTree tree)
    {
        _tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (_tree.Blackboard == null)
        {
            _tree.CreateBlackboard();
            EditorUtility.SetDirty(_tree);
            AssetDatabase.SaveAssets();
        }

        if (_tree.RootNode == null)
        {
            _tree.RootNode = _tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(_tree);
            AssetDatabase.SaveAssets();
        }

        // Create nodes views
        _tree.Nodes.ForEach(CreateNodeView);

        // Create edges views
        _tree.Nodes.ForEach(n =>
        {
            List<Node> children = _tree.GetChildren(n);
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

        if (graphviewchange.movedElements != null)
        {
            nodes.ForEach(node =>
            {
                NodeView nodeView = node as NodeView;
                nodeView.Sort();
            });
        }

        return graphviewchange;

    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        if (Application.isPlaying)
            return;
        //base.BuildContextualMenu(evt);
        Vector2 localMousePosition = this.contentViewContainer.WorldToLocal(evt.mousePosition);

        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (Type type in types)
            {
                evt.menu.AppendAction($"Add Node/{type.BaseType.Name}/{type.Name}",
                    a => CreateNode(type, localMousePosition));
                
            }
        }

        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Add Node/{type.BaseType.Name}/{type.Name}",
                    a => CreateNode(type, localMousePosition));
            }
        }

        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Add Node/{type.BaseType.Name}/{type.Name}",
                    a => CreateNode(type,localMousePosition));
            }
        }
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(port =>
            port.direction != startPort.direction &&
            port.node != startPort.node).ToList();
    }

    private void CreateNode(Type type, Vector2 position)
    {
        Node node = _tree.CreateNode(type);
        node.Position = position;
        CreateNodeView(node);
    }

    private void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }
}
