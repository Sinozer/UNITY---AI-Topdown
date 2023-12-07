// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 07/12/23
//  Author: junha
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public abstract class StateMachine
{
    protected State CurrentState;
    
    protected StateMachine(State initialState)
    {
        CurrentState = initialState;
    }
    protected StateMachine() : this(null){}
    
    void SwitchState(State state)
    {
        CurrentState?.OnExit();
        CurrentState = state;
    }
    
    void Start()
    {
        CurrentState?.OnEnter();    
    }

    void Update(float dt)
    {
        
    }
}
