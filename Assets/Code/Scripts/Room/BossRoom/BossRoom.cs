// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class BossRoom : Room
{
    public GameObject BossPrefab => _bossPrefab;
    [SerializeField] private GameObject _bossPrefab;

    public Vector3 BossSpawnPoint => _bossSpawnPoint;
    [SerializeField] Vector3 _bossSpawnPoint = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.Boss;
    }

    protected override void Update()
    {
        base.Update();
    }
}