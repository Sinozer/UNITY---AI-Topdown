// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public bool IsDead => _health <= 0 ;

    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;

    private void Awake()
    {
        _health = 100;
        _speed = 1;
        _damage = 10;
    }

    public void TakeDamage(int damage)
    {
        _health = (_health - damage < 0) ? _health - damage : 0;
    }

    public void Attack(Entity entity)
    {
        entity.TakeDamage(_damage);
    }
}