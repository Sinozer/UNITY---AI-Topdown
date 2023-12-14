// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Success,
        Failure
    }

    public State state = State.Running;
    public bool started = false;

    public State Update()
    {
        if(!started)
        {
            OnStart();
            started = true;
        }
        
        state = OnUpdate();
        
        if(state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }
    
    public abstract void OnStart();
    public abstract void OnStop();
    public abstract State OnUpdate();
}
