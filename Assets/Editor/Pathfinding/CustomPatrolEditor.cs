// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomPatrol)), CanEditMultipleObjects]
public class CustomPatrolEditor : Editor
{
    private void OnSceneGUI()
    {
        CustomPatrol patrol = target as CustomPatrol;
        if (patrol == null || patrol.enabled == false)
            return;
        Vector3[] patrolTargets = patrol.PatrolWaypoints.Waypoints;
        EditorGUI.BeginChangeCheck();
        Handles.color = Color.white;
        for (int i = 0; i < patrolTargets.Length; i++)
        {
            Vector3 newPos = Handles.PositionHandle(patrolTargets[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(patrol, "Move Patrol Target");
                patrolTargets[i] = newPos;
            }
        }
    }
}