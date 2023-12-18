// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private RoomStateManager _stateManager;

    protected bool _isLocked = true;
    public bool IsLocked
    {
        get => _isLocked;
        set => _isLocked = value;
    }

    [SerializeField]
    protected bool _isPlayerInside = false;
    public bool IsPlayerInside
    {
        get => _isPlayerInside;
        set => _isPlayerInside = value;
    }

    protected bool _hasBeenEntered = false;
    public bool HasBeenEntered
    {
        get => _hasBeenEntered;
        set => _hasBeenEntered = value;
    }

    protected bool _hasBeenSetup = false;
    public bool HasBeenSetup
    {
        get => _hasBeenSetup;
        set => _hasBeenSetup = value;
    }

    protected bool _isPlaying = false;
    public bool IsPlaying
    {
        get => _isPlaying;
        set => _isPlaying = value;
    }

    protected bool _hasBeenPlayed = false;
    public bool HasBeenPlayed
    {
        get => _hasBeenPlayed;
        set => _hasBeenPlayed = value;
    }

    protected bool _isEnded = false;
    public bool IsEnded
    {
        get => _isEnded;
        set => _isEnded = value;
    }

    public enum ERoomType
    {
        Join,           // Room that is used to join the level
        Idle,           // Room that does nothing
        Combat,         // Room that is used to fight
        Treasure,       // Room that is used to get a treasure
        Boss,           // Room that is used to fight a boss
        End,            // Room that is used to end the level
    }
    [SerializeField] protected ERoomType _roomType;
    public ERoomType RoomType => _roomType;

    [SerializeField] protected Room _nextRoom;

    [SerializeField] protected List<GameObject> _gates;
    public List<GameObject> Gates => _gates;
    public Room NextRoom => _nextRoom;

    protected virtual void Start()
    {
        _stateManager = new RoomStateManager(this);
    }

    protected virtual void Update()
    {
        _stateManager.Update();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerInside = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerInside = false;
    }
}