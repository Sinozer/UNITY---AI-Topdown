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
        if (EnemyBrain.NPC)
        {
            int randomInt = Random.Range(0, 100);
            if (randomInt < 20)
                Instantiate(GameManager.Instance.FirstAid, transform.position, Quaternion.identity);
        }

        Destroy(transform.root.gameObject);
    }

    public void DieSfx()
    {
        EnemyBrain.AudioManager.PlaySFX("Death");
    }
}