// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Security.Cryptography;
using UnityEngine;

public class CoffinNode : ActionNode
{
    private bool _isDead;
    
    public override void OnStart()
    {
        if (!Blackboard.TryFind("IsDead", out _isDead)) return;
    }

    public override void OnStop()
    {
       
    }

    public override State OnUpdate()
    {
            Blackboard.TryFind("Self", out GameObject _self);
            Destroy(_self);
            return State.Success;
    }
}