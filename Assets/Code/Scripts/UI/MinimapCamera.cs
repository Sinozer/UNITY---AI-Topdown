// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 19/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [Header("Minimap Camera")] [SerializeField]
    private Transform _playerRef;
    public float PlayerOffset = 10f;
    
    private void Start()
    {
        _playerRef = GameManager.Instance.Player?.gameObject.transform;
    }

    void Update()
    {
        if (_playerRef == null)
        {
            _playerRef = GameManager.Instance.Player.gameObject.transform;
        }
        else
        {
            transform.position = new Vector3(_playerRef.position.x, _playerRef.position.y , PlayerOffset);
        }
    }
}