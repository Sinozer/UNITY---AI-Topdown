// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Enemy : Entity
{
    public float DistFromPlayer => _distFromPlayer;

    protected float _distFromPlayer;
    private Vector3 GetPlayerPos()
    {
        Vector3 returnValue = Vector3.zero;

        Player player = GameManager.Instance.GetPlayer();

        if (player != null)
            returnValue = player.transform.position;

        return returnValue;
    }

    protected float CalculateDistFromPlayer()
    {
        return Vector2.Distance(GetPlayerPos(), transform.position);
    }
}