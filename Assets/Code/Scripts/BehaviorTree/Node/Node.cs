// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Success,
        Failure
    }

    public State CurrentState = State.Running;
    public bool Started = false;

    public State Update()
    {
        if(!Started)
        {
            OnStart();
            Started = true;
        }
        
        CurrentState = OnUpdate();
        
        if(CurrentState == State.Failure || CurrentState == State.Success)
        {
            OnStop();
            Started = false;
        }

        return CurrentState;
    }
    
    public abstract void OnStart();
    public abstract void OnStop();
    public abstract State OnUpdate();
}
