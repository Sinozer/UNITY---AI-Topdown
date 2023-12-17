// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //


using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public Node Node;
    public Port InputPort;
    public Port OutputPort;
    
    public NodeView(Node node)
    {
        Node = node;
        title = node.name;
        viewDataKey = node.Guid;
        
        style.left = node.Position.x;
        style.top = node.Position.y;
        
        CreateInputPorts();
        CreateOutputPorts();
    }
    
    private void CreateInputPorts()
    {
        switch (Node)
        {
            case RootNode:
                break;
            case ActionNode:
                InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case CompositeNode:
                InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case DecoratorNode:
                InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (InputPort == null) return;
        InputPort.portName = "Input";
        inputContainer.Add(InputPort);
    }
    
    private void CreateOutputPorts()
    {
        switch (Node)
        {
            case ActionNode:
                break;
            case CompositeNode:
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode:
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                OutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }
        if (OutputPort == null) return;
        OutputPort.portName = "Output";
        outputContainer.Add(OutputPort);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Node.Position = newPos.position;
    }
    
    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }
}