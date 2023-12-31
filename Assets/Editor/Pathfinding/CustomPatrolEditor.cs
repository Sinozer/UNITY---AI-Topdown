// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityPatrol)), CanEditMultipleObjects]
public class CustomPatrolEditor : Editor
{
    private void OnSceneGUI()
    {
        EntityPatrol patrol = target as EntityPatrol;
        if (patrol == null || patrol.enabled == false)
            return;
        List<Vector3> patrolTargets = patrol.Waypoints;
        EditorGUI.BeginChangeCheck();
        Handles.color = Color.white;
        for (int i = 0; i < patrolTargets.Count; i++)
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