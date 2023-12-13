// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private Movement _movement;
    private Vector3 GetPlayerPos()
    {
        string playerTag = "Player";
        GameObject player = GameObject.FindGameObjectsWithTag(playerTag)[0];

        return player.transform.position;
    }
}