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
    [SerializeField] private FlipSprite _flipSpriteComponent;
    
    private EdgeCollider2D _edgeCollider;
    private Vector2[] _originalPoints;
    private Enemy _data;
    private Vector2 _forWard;
    void Start()
    {
        _data = GetComponentInParent<Enemy>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _originalPoints = _edgeCollider.points;
    }

    private void Update()
    {
        if (_flipSpriteComponent.IsFlipped)
        {
            Flip();
        }else FlipBack();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    private void Flip()
    {
        Vector2[] flippedPoints = new Vector2[_originalPoints.Length];
        for (int i = 0; i < _originalPoints.Length; i++)
        {
            flippedPoints[i] = new Vector2(-_originalPoints[i].x, _originalPoints[i].y);
        }
        _edgeCollider.points = flippedPoints;
    }

    private void FlipBack()
    {
        Vector2[] flippedPoints = new Vector2[_originalPoints.Length];
        for (int i = 0; i < _originalPoints.Length; i++)
        {
            flippedPoints[i] = _originalPoints[i];
        }
        _edgeCollider.points = flippedPoints;
    }
}