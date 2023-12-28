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
    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField] protected float _maxHealth;

    // The damage the entity can deal
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    [SerializeField] protected float _damage;

    // The speed at which the entity can move
    public float MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }
    [SerializeField] protected float _movementSpeed;

    // The speed at which the entity can attack
    public float AttackSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = value;
    }
    [SerializeField] protected float _attackSpeed;

    // The range at which the entity can attack
    public float AttackRange
    {
        get => _attackRange;
        set => _attackRange = value;
    }
    [SerializeField] protected float _attackRange;

    // The range at which the entity can see
    public float VisionRange
    {
        get => _visionRange;
        set => _visionRange = value;
    }
    [SerializeField] protected float _visionRange;
}