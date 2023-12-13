// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Room : MonoBehaviour
{
    private RoomStateManager _stateManager;

    protected bool _isLocked = true;
    public bool IsLocked => _isLocked;

    protected bool _hasBeenEntered = false;
    public bool HasBeenEntered => _hasBeenEntered;

    protected bool _hasBeenSetup = false;
    public bool HasBeenSetup => _hasBeenSetup;

    protected bool _isPlaying = false;
    public bool IsPlaying => _isPlaying;

    protected bool _hasBeenPlayed = false;
    public bool HasBeenPlayed => _hasBeenPlayed;

    protected bool _isEnded = false;
    public bool IsEnded => _isEnded;

    public enum ERoomType
    {
        Idle,
        Combat,
        Treasure,
        Boss,
    }
    [SerializeField] protected ERoomType _roomType;
    public ERoomType RoomType => _roomType;

    protected virtual void Start()
    {
        _stateManager = new RoomStateManager(this);
    }

    protected virtual void Update()
    {
        _stateManager.Update();

    }
}