// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ProjectileLight : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        if (_light2D == null)
            return;

        _light2D.lightCookieSprite = _spriteRenderer.sprite;
    }
}