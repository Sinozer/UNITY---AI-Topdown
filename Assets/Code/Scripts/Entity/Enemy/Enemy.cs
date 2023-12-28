// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Enemy : Entity
{
    public float DistFromPlayer => CalculateDistFromPlayer();

    public Vector3 GetPlayerPos()
    {
        Vector3 returnValue = Vector3.zero;

        Player player = GameManager.Instance.Player;

        if (player != null)
            returnValue = player.transform.position;

        return returnValue;
    }

    public float CalculateDistFromPlayer()
    {
        return Vector2.Distance(GetPlayerPos(), transform.position);
    }
}