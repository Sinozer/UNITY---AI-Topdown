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
   [FormerlySerializedAs("nodes")] public List<Node> Nodes = new List<Node>();
   
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
}