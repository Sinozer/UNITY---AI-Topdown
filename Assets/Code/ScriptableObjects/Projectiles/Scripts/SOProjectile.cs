// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "ScriptableObjects/Projectile")]
public class SOProjectile : ScriptableObject
{
    [SerializeField]
    private RuntimeAnimatorController _controller;
    public RuntimeAnimatorController Controller => _controller;

    [SerializeField]
    private float _speed;
    public float Speed => _speed;

    [SerializeField]
    private float _damage;
    public float Damage => _damage;

    [SerializeField]
    private float _lifeTime;
    public float LifeTime => _lifeTime;
}