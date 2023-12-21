// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 13/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    [SerializeField] protected SOEntity _baseData;
    public float DistFromPlayer => CalculateDistFromPlayer();

    protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;
    protected virtual void Awake()
    {

        if (_baseData == null)
        {
            // NOT TESTED
            if (GameManager.Instance.EntityList.List.ContainsKey(name))
                _baseData = GameManager.Instance.EntityList.List[name];
            else
                _baseData = GameManager.Instance.EntityList.List.First().Value;
        }

        _health = _baseData.MaxHealth;
        _maxHealth = _baseData.MaxHealth;
        _damage = _baseData.Damage;
        _movementSpeed = _baseData.MovementSpeed;
        _attackSpeed = _baseData.AttackSpeed;
        _attackRange = _baseData.AttackRange;
        _visionRange = _baseData.VisionRange;
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    
    public Vector3 GetPlayerPos()
    {
        Vector3 returnValue = Vector3.zero;

        Player player = GameManager.Instance.Player;

        if (player != null)
            returnValue = player.transform.position;

        return returnValue;
    }

    public float CalculateDistFromPlayer()
    {
        return Vector2.Distance(GetPlayerPos(), transform.position);
    }
}