// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
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

    public BossBrain BossBrain => _bossBrain;
    private BossBrain _bossBrain;

    public SOEntity BaseData
    {
        get => _baseData;
        set => _baseData = value;
    }
    [SerializeField, DisableIf("@true")] private SOEntity _baseData;

    private PhaseStateManager _stateManager;

    // Used by the state manager
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set => _isUnlocked = value;
    }
    private bool _isUnlocked = false;

    // Used by the state manager
    public bool IsEnded
    {
        get => _isEnded;
        set => _isEnded = value;
    }
    private bool _isEnded = false;

    protected virtual void Start()
    {
        _stateManager = new PhaseStateManager(this);

        _bossBrain = transform.root.GetComponentInChildren<BossBrain>();
    }

    protected virtual void Update()
    {
        _stateManager.Update();
    }
}