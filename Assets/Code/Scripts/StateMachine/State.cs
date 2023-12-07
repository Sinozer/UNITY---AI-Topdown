// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 07/12/23
//  Author: junha
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

public abstract class State 
{
    public virtual void OnEnter(){}
    
    public virtual void OnExit(){}
    
    public virtual void OnUpdate(float dt){}
}
