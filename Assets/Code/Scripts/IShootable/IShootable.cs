using UnityEngine;

public interface IShootable 
{
    void Shoot(Rigidbody2D rigibody, Vector2 direction, float speed);
}
