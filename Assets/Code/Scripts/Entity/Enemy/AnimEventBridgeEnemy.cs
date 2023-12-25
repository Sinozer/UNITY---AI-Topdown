// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AnimationEventBridgeEnemy : MonoBehaviour
{
    [SerializeField] protected EnemyBrain EnemyBrain;

    private void Start()
    {
        if (EnemyBrain == null)
            EnemyBrain = transform.root.GetComponentInChildren<EnemyBrain>();
    }

    public void Die()
    {
        EnemyBrain.Die();
    }

    public void DieSfx()
    {
        EnemyBrain.PlayDeathSfx();
    }
}