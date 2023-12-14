// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "BehaviorTree/BehaviorTree")]
public class BehaviourTree : ScriptableObject
{
   public Node rootNode;
   public Node.State treeState = Node.State.Running;
   
   public Node.State Update()
   {
       if(rootNode.state == Node.State.Running)
       {
           treeState = rootNode.Update();
       }

       return treeState;
   }
}