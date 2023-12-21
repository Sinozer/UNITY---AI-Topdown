// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class LaserSweep : MonoBehaviour
{
    public float sweepSpeed;
    public float maxRotation; 

    void Update()
    {
        float angle = maxRotation * Mathf.PingPong(Time.time * sweepSpeed, 1);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (!other.transform.parent.gameObject.TryGetComponent<Player>(out var player))
    //         return;
    //     
    //     player?.TakeDamage(30);
    //     
    // }
}