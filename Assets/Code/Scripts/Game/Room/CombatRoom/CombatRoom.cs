// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class CombatRoom : Room
{
    public EntitySpawner EntitySpawner => _entitySpawner;
    [SerializeField] private EntitySpawner _entitySpawner;

    protected override void Start()
    {
        base.Start();
        _roomType = ERoomType.Combat;

        if (_entitySpawner == null)
            _entitySpawner = GetComponentInChildren<EntitySpawner>();
    }

    protected override void Update()
    {
        base.Update();
    }
}