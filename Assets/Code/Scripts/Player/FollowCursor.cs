using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePosition;
    }
}
