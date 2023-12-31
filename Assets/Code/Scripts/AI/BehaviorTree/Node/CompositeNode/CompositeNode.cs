// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class CompositeNode : Node
{
    [HideInInspector] public List<Node> Children = new List<Node>();
    
    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.Children = Children.ConvertAll(child => child.Clone());
        return node;
    }
}