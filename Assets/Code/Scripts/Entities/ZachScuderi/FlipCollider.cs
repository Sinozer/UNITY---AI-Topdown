// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class FlipCollider : MonoBehaviour
{
    private EdgeCollider2D _edgeCollider;
    private Vector2[] _originalPoints;
    private Enemy _data;
    
    void Start()
    {
        _data = GetComponentInParent<Enemy>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _originalPoints = _edgeCollider.points;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    public void Flip()
    {
        Vector2[] flippedPoints = new Vector2[_originalPoints.Length];
        for (int i = 0; i < _originalPoints.Length; i++)
        {
            flippedPoints[i] = new Vector2(-_originalPoints[i].x, _originalPoints[i].y);
        }
        _edgeCollider.points = flippedPoints;
    }
}