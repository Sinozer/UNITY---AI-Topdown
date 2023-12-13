// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 07/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class ExampleObj : MonoBehaviour
{
    private ExampleStateManager _stateManager;
    public bool IsIdle;
    public bool IsDead;
    public int AttackCount;

    void Start()
    {
        _stateManager = new ExampleStateManager(this);
    }

    void Update()
    {
        _stateManager.Update();
        if (IsDead)
            Destroy(gameObject);
    }
}