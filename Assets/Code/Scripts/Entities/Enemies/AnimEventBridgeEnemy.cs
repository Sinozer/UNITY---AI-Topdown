// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class AnimationEventBridgeEnemy : MonoBehaviour
{
    [SerializeField] protected EnemyBrain EnemyBrain; // reference to your main script

    public void Die()
    {
        EnemyBrain.Die();
    }
}
