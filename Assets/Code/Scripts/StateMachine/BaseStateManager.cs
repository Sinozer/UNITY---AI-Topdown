// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 07/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public abstract class BaseStateManager<Manager, EState, Obj>
    where Manager : BaseStateManager<Manager, EState, Obj>
    where EState : System.Enum
    where Obj : MonoBehaviour
{
    protected BaseState<Manager, EState, Obj> _currentState;
    protected Obj _owner;
    private bool _isTransitioning = false;

    public Obj Owner => _owner;

    public BaseStateManager(Obj owner)
    {
        _owner = owner;
    }

    public void Update()
    {
        if (_isTransitioning)
            return;

        _currentState.OnUpdate((Manager)this);
    }

    public void ChangeState(EState state)
    {
        _isTransitioning = true;
        _currentState.OnExit((Manager)this);
        _currentState = GetState(state);
        _currentState.OnEnter((Manager)this);
        _isTransitioning = false;
    }

    protected abstract BaseState<Manager, EState, Obj> GetState(EState state);
}
