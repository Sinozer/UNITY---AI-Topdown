// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 07/12/23
//  Author: junha
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public abstract class BaseState<Manager, EState, Obj>
    where Manager : BaseStateManager<Manager, EState, Obj>
    where EState : System.Enum
    where Obj : MonoBehaviour
{
    public abstract void OnEnter(Manager manager);

    public abstract void OnExit(Manager manager);

    public abstract void OnUpdate(Manager manager);
}
