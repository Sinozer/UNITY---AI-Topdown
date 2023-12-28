using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCursor : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        transform.root.position = worldPosition;


        //Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //// Get the screen boundaries
        //float screenX = Screen.width;
        //float screenY = Screen.height;

        //// Convert screen boundaries to world space
        //Vector3 minScreenBounds = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        //Vector3 maxScreenBounds = _camera.ScreenToWorldPoint(new Vector3(screenX, screenY, _camera.nearClipPlane));

        //// Clamp the mouse position within the screen boundaries
        //mousePosition.x = Mathf.Clamp(mousePosition.x, minScreenBounds.x, maxScreenBounds.x);
        //mousePosition.y = Mathf.Clamp(mousePosition.y, minScreenBounds.y, maxScreenBounds.y);

        //transform.root.position = mousePosition;
    }
}
