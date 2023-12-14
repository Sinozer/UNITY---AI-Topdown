// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public List<Node> children = new List<Node>();
}