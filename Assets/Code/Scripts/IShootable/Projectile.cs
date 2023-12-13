// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifetime;
    public float Speed;

    void Start()
    {
        Destroy(gameObject,lifetime);
    }
}