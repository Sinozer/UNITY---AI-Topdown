// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    [SerializeField] GameObject bulletPrefab;

    public IShootable Shootable;
    public float BulletSpeed;
    public float FireRate = 2.5f;
    

    private Vector2 _direction;
    private Vector2 _playerPosition;
    private Vector2 _mousePosition;


    void Start()
    {
        BulletSpeed = bulletPrefab.GetComponent<Projectile>().Speed;
        Shootable = bulletPrefab.GetComponent<IShootable>();
    }

    private void Update()
    {
        _playerPosition = transform.position;
        _mousePosition = _aim.position;
        _direction = (_mousePosition - _playerPosition).normalized;
    }

    public void Shoot()
    {
        if (Shootable != null)
        {
            float rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletPrefab, _playerPosition, Quaternion.Euler(0, 0, rotation));
            Rigidbody2D rg = bullet.GetComponent<Rigidbody2D>();
            Shootable.Shoot(rg, _direction, BulletSpeed);
        }
    }
}
