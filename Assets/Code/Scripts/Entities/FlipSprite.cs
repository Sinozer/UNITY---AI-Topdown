// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 20/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public bool IsFlipped => _spriteRenderer.flipX; 
        
    private Vector2 _lastPosition;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _lastPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 currentPosition = transform.position;

        if (currentPosition.x > _lastPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else if (currentPosition.x < _lastPosition.x)
        {
            _spriteRenderer.flipX = true;
        }

        _lastPosition = currentPosition;
    }
}