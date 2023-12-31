// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 27/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class RoomChild : ObjectChild<Room>
{
    public Room Room => Object;

    #region Physics
    public BoxCollider2D RoomCollider
    {
        get
        {
            if (_roomCollider == null)
                _roomCollider = GetCollider2D<BoxCollider2D>("Room");

            return _roomCollider;
        }
    }
    private BoxCollider2D _roomCollider;
    #endregion Physics

    public Transform[] Gates
    {
        get
        {
            if (_gates == null)
                _gates = Room.transform.Find("Gates").GetComponentsInChildren<Transform>();

            return _gates;
        }
    }
    private Transform[] _gates;
}