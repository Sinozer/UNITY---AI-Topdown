// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class JoinRoom : Room
{
    [SerializeField] private GameObject _playerPrefab;
    public GameObject PlayerPrefab => _playerPrefab;

    protected override void Start()
    {
        base.Start();
        
        _roomType = ERoomType.Join;
        _isLocked = false;
        _isPlayerInside = true;
    }

    protected override void Update()
    {
        base.Update();
    }
}