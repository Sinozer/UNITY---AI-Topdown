// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public class MinimapCamera : MonoBehaviour
{
    [Header("Minimap Camera")]
    [SerializeField] private Transform _playerRef;

    [SerializeField] private float _playerOffset = 10f;
    [SerializeField] private float _zoom;
    
    public void Zoom(float zoom)
    {
        _zoom = zoom;
        GetComponent<Camera>().orthographicSize = _zoom;
    }
    
    public void ActivateSeeAll()
    {
        _playerOffset = -_playerOffset;
    }
    
    public void DeactivateSeeAll()
    {
        _playerOffset = Mathf.Abs(_playerOffset);
    }
    
}