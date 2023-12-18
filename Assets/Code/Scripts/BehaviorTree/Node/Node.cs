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

    [HideInInspector] public State CurrentState = State.Running;
    [HideInInspector] public bool Started = false;

    [HideInInspector] public string Guid;
    [HideInInspector] public Vector2 Position;
    public CustomBlackboard Blackboard;
    
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
    
    public virtual Node Clone()
    {
        Node node = Instantiate(this);
        return node;
    }
    
    public abstract void OnStart();
    public abstract void OnStop();
    public abstract State OnUpdate();
}
