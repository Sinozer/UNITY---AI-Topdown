// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/BehaviorTree")]
public class BehaviourTree : ScriptableObject
{
    public Node RootNode;
    public Node.State TreeState = Node.State.Running;
    public List<Node> Nodes = new List<Node>();
   
    public Node.State Update()
    {
        if(RootNode.CurrentState == Node.State.Running)
        {
            TreeState = RootNode.Update();
        }
        return TreeState;
    }

    public Node CreateNode(Type type)
    {
        Node node = CreateInstance(type) as Node;
        node.name = type.Name;
        node.Guid = GUID.Generate().ToString();

        Undo.RecordObject(this, "Create Node");
        Nodes.Add(node);
        AssetDatabase.AddObjectToAsset(node, this);
        Undo.RegisterCreatedObjectUndo(node, "Create Node");
        AssetDatabase.SaveAssets();
        return node;
    }
   
    public void DeleteNode(Node node)
    {
        Undo.RecordObject(this, "Delete Node");
        Nodes.Remove(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }
   
    public void AddChild(Node parent, Node child)
    {
        switch (parent)
        {
            case RootNode rootNode:
                Undo.RecordObject(rootNode, "Add Child");
                rootNode.Child = child;
                EditorUtility.SetDirty(rootNode);
                return;
            case DecoratorNode decoratorNode:
                Undo.RecordObject(decoratorNode, "Add Child");
                decoratorNode.Child = child;
                EditorUtility.SetDirty(decoratorNode);
                return;
            case CompositeNode compositeNode:
                Undo.RecordObject(compositeNode, "Add Child");
                compositeNode.Children.Add(child);
                EditorUtility.SetDirty(compositeNode);
                return;
        }
    }
   
    public void RemoveChild(Node parent, Node child)
    {
        switch(parent)
        {
            case RootNode rootNode:
                Undo.RecordObject(rootNode, "Remove Child");
                rootNode.Child = null;
                EditorUtility.SetDirty(rootNode);
                return;
            case DecoratorNode decoratorNode:
                Undo.RecordObject(decoratorNode, "Remove Child");
                decoratorNode.Child = null;
                EditorUtility.SetDirty(decoratorNode);
                return;
            case CompositeNode compositeNode:
                Undo.RecordObject(compositeNode, "Remove Child");
                compositeNode.Children.Remove(child);
                EditorUtility.SetDirty(compositeNode);
                return;
        
        }
    }
   
    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();
       
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if(decoratorNode && decoratorNode.Child != null)
        {
            children.Add(decoratorNode.Child);
        }
       
        RootNode rootNode = parent as RootNode;
        if(rootNode && rootNode.Child != null)
        {
            children.Add(rootNode.Child);
        }
         
        CompositeNode compositeNode = parent as CompositeNode;
        return compositeNode ? compositeNode.Children : children;
    }
   
    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.RootNode = RootNode.Clone();
        tree.Nodes = new List<Node>();
        TraverseTree(tree.RootNode, node =>
        {
            tree.Nodes.Add(node);
        });

        return tree;
    }

    private void TraverseTree(Node node, Action<Node> nodeVisitor)
    {
        if (node == null) return;
        nodeVisitor.Invoke(node);
        GetChildren(node).ForEach(n => TraverseTree(n, nodeVisitor));
    }
}