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

    public IShootable Shootable;
    public float BulletSpeed;
    public float FireRate = 2.5f;
    
    private float _fireTimer = 0;
    private bool _canFire = false;
    private Rigidbody2D _bulletRigidbody;

    void Start()
    {
        _fireTimer = 0;
        _bulletRigidbody = bulletPrefab.GetComponent<Rigidbody2D>();
        BulletSpeed = bulletPrefab.GetComponent<Projectile>().Speed;
        Shootable = bulletPrefab.GetComponent<IShootable>();
    }

    private void Update()
    {
        if (!_canFire)
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0)
            {
                _canFire = true;
            }
        }
    }

    public void Shoot(Vector2 direction)
    {
        if (_canFire && Shootable != null)
        {
            Shootable.Shoot(_bulletRigidbody, direction, BulletSpeed);
            _canFire = false;
            _fireTimer = 1 / FireRate;
        }
    }
}
