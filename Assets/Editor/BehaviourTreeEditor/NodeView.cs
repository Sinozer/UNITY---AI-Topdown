// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //


using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public Node Node;
    public Port InputPort;
    public Port OutputPort;
    
    public NodeView(Node node) : base("Assets/Editor/BehaviourTreeEditor/NodeView.uxml")
    {
        Node = node;
        title = node.name;
        viewDataKey = node.Guid;
        
        style.left = node.Position.x;
        style.top = node.Position.y;
        
        CreateInputPorts();
        CreateOutputPorts();
        AddStyleClass();
    }

    private void AddStyleClass()
    {
        switch(Node)
        {
            case RootNode:
                AddToClassList("rootNode");
                break;
            case ActionNode:
                AddToClassList("actionNode");
                break;
            case CompositeNode:
                AddToClassList("compositeNode");
                break;
            case DecoratorNode:
                AddToClassList("decoratorNode");
                break;
        }
    }
    
    private void CreateInputPorts()
    {
        switch (Node)
        {
            case RootNode:
                break;
            case ActionNode:
                InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case CompositeNode:
                InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case DecoratorNode:
                InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (InputPort == null) return;
        InputPort.portName = "";
        InputPort.style.flexDirection = FlexDirection.Column;
        inputContainer.Add(InputPort);
    }
    
    private void CreateOutputPorts()
    {
        switch (Node)
        {
            case ActionNode:
                break;
            case CompositeNode:
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode:
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }
        if (OutputPort == null) return;
        OutputPort.portName = "";
        OutputPort.style.flexDirection = FlexDirection.ColumnReverse;
        outputContainer.Add(OutputPort);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(Node, "Node Moved");
        Node.Position = newPos.position;
        EditorUtility.SetDirty(Node);
    }
    
    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }

    public void UpdateNodeGUI()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("success");
        RemoveFromClassList("failure");

        if (!Application.isPlaying) 
            return;

        switch (Node.CurrentState)
        {
            case Node.State.Running:
                if (Node.Started)
                    AddToClassList("running");
                break;
            case Node.State.Success:
                AddToClassList("success");
                break;
            case Node.State.Failure:
                AddToClassList("failure");
                break;
        }
    }
}