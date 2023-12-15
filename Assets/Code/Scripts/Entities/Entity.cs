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
    [SerializeField] protected int _damage;
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _visionRange;

    private void Awake()
    {

    }

    public void TakeDamage(int damage)
    {
        _health = (_health - damage < 0) ? _health - damage : 0;
    }

    public void Attack(Entity entity)
    {
        entity.TakeDamage(_damage);
    }

    public void Die(float time)
    {
        Destroy(transform.parent.gameObject, time);
    }
}