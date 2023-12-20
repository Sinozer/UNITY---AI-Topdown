// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class FlipCollider : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    private Vector2[] originalPoints;

    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        originalPoints = edgeCollider.points;
    }
    

    public void Flip()
    {
        Vector2[] flippedPoints = new Vector2[originalPoints.Length];
        for (int i = 0; i < originalPoints.Length; i++)
        {
            flippedPoints[i] = new Vector2(-originalPoints[i].x, originalPoints[i].y);
        }
        edgeCollider.points = flippedPoints;
    }
}