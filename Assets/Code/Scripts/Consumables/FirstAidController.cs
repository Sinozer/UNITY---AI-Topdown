// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class FirstAidController : MonoBehaviour
{
    [SerializeField] private float _yOffset = 0.005f;
    [SerializeField] private int HealthAmount = 10;

    internal void HealPlayer(Player player)
    {
        player.Heal(HealthAmount);
        Destroy(transform.root.gameObject);
    }

    private void Update()
    {
        transform.parent.position += Vector3.up * Mathf.Sin(Time.time * 2) * _yOffset;
    }
}