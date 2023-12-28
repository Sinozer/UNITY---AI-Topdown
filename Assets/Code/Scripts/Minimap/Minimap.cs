// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 28/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Minimap : ObjectChild<Minimap>
{
    public Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = GetExternal<Camera>();
            return _camera;
        }
    }
    private Camera _camera;

    public float OrthographicSize
    {
        get => _orthographicSize;
        set
        {
            _orthographicSize = value;
            Camera.orthographicSize = _orthographicSize;
        }
    }
    private float _orthographicSize;

    private void Awake()
    {
        OrthographicSize = Camera.orthographicSize;
    }
}