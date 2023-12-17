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
using UnityEngine.Serialization;

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
       Nodes.Add(node);
       
       AssetDatabase.AddObjectToAsset(node, this);
       AssetDatabase.SaveAssets();
       return node;
   }
   
   public void DeleteNode(Node node)
   {
       Nodes.Remove(node);
         AssetDatabase.RemoveObjectFromAsset(node);
         AssetDatabase.SaveAssets();
   }
   
   public void AddChild(Node parent, Node child)
   {
       DecoratorNode decoratorNode = parent as DecoratorNode;
         if(decoratorNode)
         {
              decoratorNode.Child = child;
         }
         
         RootNode rootNode = parent as RootNode;
         if(rootNode)
         {
             rootNode.Child = child;
         }
         
         CompositeNode compositeNode = parent as CompositeNode;
         if (compositeNode)
         {
             compositeNode.Children.Add(child);
         }
   }
   
   public void RemoveChild(Node parent, Node child)
   {
       DecoratorNode decoratorNode = parent as DecoratorNode;
       if(decoratorNode)
       {
           decoratorNode.Child = null;
       }
         
       RootNode rootNode = parent as RootNode;
       if(rootNode)
       {
           rootNode.Child = null;
       }
       
       CompositeNode compositeNode = parent as CompositeNode;
       if (compositeNode)
       {
           compositeNode.Children.Remove(child);
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
       return tree;
   }
   
}