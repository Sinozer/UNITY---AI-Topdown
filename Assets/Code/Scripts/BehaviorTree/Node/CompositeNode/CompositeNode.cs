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
    public List<Node> Children = new List<Node>();
}