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
    public float sweepSpeed;
    public float minValue = -60;
    public float maxValue = 60;  
    
    float range => maxValue - minValue;
    float midPoint => (maxValue + minValue) / 2;
    
    void Update()
    {
        float angle = midPoint + (Mathf.PingPong(Time.time * sweepSpeed, range) - range / 2);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        if (!other.transform.parent.gameObject.TryGetComponent<Player>(out var player))
            return;
        
        player?.TakeDamage(30);
        
    }
}