// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 17/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class SOEntity : ScriptableObject
{
    // The health of the entity
    public float MaxHealth => _maxHealth;
    [SerializeField] protected float _maxHealth;

    // The damage the entity can deal
    public float Damage => _damage;
    [SerializeField] protected float _damage;

    // The speed at which the entity can move
    public float MovementSpeed => _movementSpeed;
    [SerializeField] protected float _movementSpeed;

    // The speed at which the entity can attack
    public float AttackSpeed => _attackSpeed;
    [SerializeField] protected float _attackSpeed;

    // The range at which the entity can attack
    public float AttackRange => _attackRange;
    [SerializeField] protected float _attackRange;

    // The range at which the entity can see
    public float VisionRange => _visionRange;
    [SerializeField] protected float _visionRange;
}