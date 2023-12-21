// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class Phase : MonoBehaviour
{
    #region Events
    public event Action OnPhaseUnlockedStarting;
    public void InvokeOnPhaseUnlockedStarting()
    {
        OnPhaseUnlockedStarting?.Invoke();
    }
    
    public event Action OnPhaseSetupStarting;
    public void InvokeOnPhaseSetupStarting()
    {
        OnPhaseSetupStarting?.Invoke();
    }
    
    public event Action OnPhasePlayingStarting;
    public void InvokeOnPhasePlayingStarting()
    {
        OnPhasePlayingStarting?.Invoke();
    }
    
    public event Action OnPhaseEndedStarting;
    public void InvokeOnPhaseEndedStarting()
    {
        OnPhaseEndedStarting?.Invoke();
    }
    #endregion Events

    private PhaseStateManager _stateManager;

    // Used by the state manager
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set => _isUnlocked = value;
    }
    private bool _isUnlocked;

    // Used by the state manager
    public bool HasBeenSetup
    {
        get => _hasBeenSetup;
        set => _hasBeenSetup = value;
    }
    private bool _hasBeenSetup;

    // Used by the state manager
    public bool IsEnded
    {
        get => _isEnded;
        set => _isEnded = value;
    }
    private bool _isEnded;

    // Managed by the state manager
    public bool IsPlaying
    {
        get => _isPlaying;
        set => _isPlaying = value;
    }
    private bool _isPlaying;

    protected virtual void Start()
    {
        _stateManager = new PhaseStateManager(this);
    }

    protected virtual void Update()
    {
        _stateManager.Update();
    }
}