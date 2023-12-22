// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

abstract public class EntityChild : MonoBehaviour
{
    public Entity Entity
    {
        get
        {
            if (_entity == null)
                _entity = transform.root.GetComponentInChildren<Entity>();

            return _entity;
        }
    }
    private Entity _entity;

    public GameObject Render
    {
        get
        {
            if (_render == null)
                _render = transform.root.Find("Render").gameObject;

            return _render;
        }
    }
    private GameObject _render;

    public GameObject Physics
    {
        get
        {
            if (_physics == null)
                _physics = transform.root.Find("Physics").gameObject;

            return _physics;
        }
    }
    private GameObject _physics;

    public GameObject Actions
    {
        get
        {
            if (_actions == null)
                _actions = transform.root.Find("Actions").gameObject;

            return _actions;
        }
    }
    private GameObject _actions;

    #region Components
    public Rigidbody2D Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = transform.root.GetComponentInChildren<Rigidbody2D>();

            return _rigidbody;
        }
    }
    private Rigidbody2D _rigidbody;

    public Animator Animator
    {
        get
        {
            if (_animator == null)
                _animator = transform.root.GetComponentInChildren<Animator>();

            return _animator;
        }
    }
    private Animator _animator;
    #endregion Components

    #region Checks
    public bool NPC => Entity.IsNpc;
    public bool NotNPC => !Entity.IsNpc;
    public bool Alive => Entity.IsAlive;
    public bool Dead => Entity.IsDead;
    #endregion Checks
}