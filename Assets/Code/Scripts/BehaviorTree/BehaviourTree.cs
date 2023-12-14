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
   public Node RootNode;
   public Node.State TreeState = Node.State.Running;
   
   public Node.State Update()
   {
       if(RootNode.CurrentState == Node.State.Running)
       {
           TreeState = RootNode.Update();
       }

       return TreeState;
   }
}