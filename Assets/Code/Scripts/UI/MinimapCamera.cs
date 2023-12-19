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
    
    void Update()
    {
        if (_playerRef != null)
        {
            var position = _playerRef.position;
            transform.position = new Vector3(position.x, position.y, PlayerOffset);
        }
        else
        {
            if (GameManager.Instance.Player == null) return;
            _playerRef = GameManager.Instance.Player.gameObject.transform;
        }
    }
}