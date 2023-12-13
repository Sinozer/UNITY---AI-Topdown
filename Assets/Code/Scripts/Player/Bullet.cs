// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float Lifetime = 10f; // Bullet lifetime set to 10 seconds

    void Start()
    {
        Destroy(gameObject, Lifetime); // Destroy the bullet after "lifetime" seconds
    }
}