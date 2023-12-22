// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class IConsume : MonoBehaviour
{
    [SerializeField] private FirstAidController _controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.transform.root.TryGetComponent<Player>(out var player))
            return;

        _controller.HealPlayer(player);
    }
}
