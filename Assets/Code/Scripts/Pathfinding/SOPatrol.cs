// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 15/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "Patrol", menuName = "AI/Patrol", order = 1)]
public class SOPatrol : ScriptableObject
{
    public Vector3[] Waypoints;
    public float Delay = 0;
}