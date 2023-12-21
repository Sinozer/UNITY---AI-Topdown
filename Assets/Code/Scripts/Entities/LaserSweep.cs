// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 21/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class LaserSweep : MonoBehaviour
{
    public float SweepSpeed;
    public float MinValue = -60;
    public float MaxValue = 60;  
    public float Damage = 30;
    
    float range => MaxValue - MinValue;
    float midPoint => (MaxValue + MinValue) / 2;
    
    void Update()
    {
        float angle = midPoint + (Mathf.PingPong(Time.time * SweepSpeed, range) - range / 2);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        if (!other.transform.parent.gameObject.TryGetComponent<Player>(out var player))
            return;
        
        player?.TakeDamage(Damage);
        
    }
}