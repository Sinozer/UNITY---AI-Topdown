// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class TankyBrain : Enemy
{
    public bool SeePlayer => _seePlayer;

    private TankyStateManager _stateManager;
    [SerializeField] private bool _seePlayer = false;

    private void Awake()
    {
        _health = 150;
        _movementSpeed = 0.6f;
        _attackSpeed = 1.1f;
        _damage = 6;
        _attackRange = 10;

        _stateManager = new TankyStateManager(this);
    }

    private void Update()
    {
        _stateManager.Update();
        if (IsDead)
            Destroy(gameObject);
    }
}