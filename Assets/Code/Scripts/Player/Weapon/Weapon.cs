// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    public IShootable shootable;
    public float BulletSpeed;

    private float _fireRate = 1f;
    private Rigidbody2D _bulletRigidbody;

    void Start()
    {
        _bulletRigidbody = bulletPrefab.GetComponent<Rigidbody2D>();
        shootable = bulletPrefab.GetComponent<IShootable>();
        BulletSpeed = bulletPrefab.GetComponent<Projectile>().Speed;
    }

    public void Shoot(Vector2 direction)
    {
        shootable.Shoot(_bulletRigidbody, direction, BulletSpeed);
    }
}