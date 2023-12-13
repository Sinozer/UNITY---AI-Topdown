// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 10f; // Bullet lifetime set to 10 seconds

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after "lifetime" seconds
    }
}