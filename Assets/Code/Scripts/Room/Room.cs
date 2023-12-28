// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : RoomChild
{
    #region RoomType
    public enum ERoomType
    {
        Join,           // Room that is used to join the level
        Idle,           // Room that does nothing
        Combat,         // Room that is used to fight
        Treasure,       // Room that is used to get a treasure
        Boss,           // Room that is used to fight a boss
        End,            // Room that is used to end the level
    }
    /// <summary>
    /// The type of the room.
    /// </summary>
    public ERoomType RoomType => _roomType;
    [SerializeField] protected ERoomType _roomType;
    #endregion RoomType

    #region StateManager
    private RoomStateManager _stateManager;

    public bool IsLocked
    {
        get => _isLocked;
        set => _isLocked = value;
    }
    [SerializeField] protected bool _isLocked = true;

    public bool IsPlayerInside
    {
        get => _isPlayerInside;
        set => _isPlayerInside = value;
    }
    [SerializeField] protected bool _isPlayerInside = false;

    public bool HasBeenEntered
    {
        get => _hasBeenEntered;
        set => _hasBeenEntered = value;
    }
    protected bool _hasBeenEntered = false;

    public bool HasBeenSetup
    {
        get => _hasBeenSetup;
        set => _hasBeenSetup = value;
    }
    protected bool _hasBeenSetup = false;

    public bool IsPlaying
    {
        get => _isPlaying;
        set => _isPlaying = value;
    }
    protected bool _isPlaying = false;

    public bool HasBeenPlayed
    {
        get => _hasBeenPlayed;
        set => _hasBeenPlayed = value;
    }
    protected bool _hasBeenPlayed = false;

    public bool IsEnded
    {
        get => _isEnded;
        set => _isEnded = value;
    }
    protected bool _isEnded = false;
    #endregion StateManager

    #region Tilemaps
    /// <summary>
    /// The next room gameobject to go to.
    /// </summary>
    public Room NextRoom => _nextRoom;
    [SerializeField] protected Room _nextRoom;

    /// <summary>
    /// The floor tilemap.
    /// </summary>
    public Tilemap FloorTilemap
    {
        get
        {
            if (_floorTilemap == null)
                _floorTilemap = GameObject.Find("Floor").GetComponent<Tilemap>();

            return _floorTilemap;
        }
    }
    private Tilemap _floorTilemap;

    /// <summary>
    /// The water tilemap.
    /// </summary>
    public Tilemap WaterTilemap
    {
        get
        {
            if (_waterTilemap == null)
                _waterTilemap = GameObject.Find("Water").GetComponent<Tilemap>();

            return _waterTilemap;
        }
    }
    private Tilemap _waterTilemap;

    /// <summary>
    /// The decoration collider tilemap.
    /// </summary>
    public Tilemap DecorationColliderTilemap
    {
        get
        {
            if (_decorationColliderTilemap == null)
                _decorationColliderTilemap = GameObject.Find("DecorationCollider").GetComponent<Tilemap>();

            return _decorationColliderTilemap;
        }
    }
    private Tilemap _decorationColliderTilemap;

    /// <summary>
    /// The no spawn tilemap.
    /// </summary>
    public Tilemap NoSpawnTilemap
    {
        get
        {
            if (_noSpawnTilemap == null)
                _noSpawnTilemap = GameObject.Find("NoSpawn").GetComponent<Tilemap>();

            return _noSpawnTilemap;
        }
    }
    private Tilemap _noSpawnTilemap;
    #endregion Tilemaps

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
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        if (gameObject.layer != LayerMask.NameToLayer("Room"))
            return;

        _isPlayerInside = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        if (gameObject.layer != LayerMask.NameToLayer("Room"))
            return;

        _isPlayerInside = false;
    }

    /// <summary>
    /// Get a random position inside the room.
    /// </summary>
    /// <returns> Vector3 with a random position inside the room. </returns>
    public Vector3 GetRandomPosition()
    {
        var x = Random.Range(RoomCollider.bounds.min.x, RoomCollider.bounds.max.x);
        var y = Random.Range(RoomCollider.bounds.min.y, RoomCollider.bounds.max.y);

        return new Vector3(x, y, Room.transform.position.z);
    }

    /// <summary>
    /// Check if the position is on the floor and not in the water.
    /// </summary>
    /// <param name="value"> The position to check. </param>
    /// <returns> True if the position is on the floor and not in the water. </returns>
    public bool IsValidPosition(Vector3 value)
    {
        var floor = FloorTilemap.GetTile(_floorTilemap.WorldToCell(value));
        var water = WaterTilemap.GetTile(_waterTilemap.WorldToCell(value));
        var decorationCollider = DecorationColliderTilemap.GetTile(_decorationColliderTilemap.WorldToCell(value));
        var noSpawn = NoSpawnTilemap.GetTile(_noSpawnTilemap.WorldToCell(value));

        return floor != null && water == null && decorationCollider == null && noSpawn == null;
    }

    /// <summary>
    /// Get a random valid position inside the room.
    /// </summary>
    /// <returns> Vector3 with a random valid position inside the room. </returns>
    public Vector3 GetRandomValidPositionInRoom()
    {
        while (true)
        {
            Vector3 returnValue = GetRandomPosition();

            if (IsValidPosition(returnValue) == false)
                continue;

            return returnValue;
        }
    }
}