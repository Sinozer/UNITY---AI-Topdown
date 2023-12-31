// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public abstract class DecoratorNode : Node
{
   [HideInInspector] public Node Child;
   
   public override Node Clone()
   {
      DecoratorNode node = Instantiate(this);
      node.Child = Child.Clone();
      return node;
   }
}