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

    public AudioManager AudioManager
    {
        get
        {
            if (_audioManager == null)
                _audioManager = transform.root.GetComponentInChildren<AudioManager>();

            return _audioManager;
        }
    }
    private AudioManager _audioManager;

    public VFXManager VFXManager
    {
        get
        {
            if (_vfxManager == null)
                _vfxManager = transform.root.GetComponentInChildren<VFXManager>();

            return _vfxManager;
        }
    }
    private VFXManager _vfxManager;

    public GameObject Actions
    {
        get
        {
            if (_actions == null)
                _actions = _entity.transform.Find("Actions").gameObject;

            return _actions;
        }
    }
    private GameObject _actions;

    public GameObject Render
    {
        get
        {
            if (_render == null)
                _render = _entity.transform.Find("Render").gameObject;

            return _render;
        }
    }
    private GameObject _render;

    public GameObject Physics
    {
        get
        {
            if (_physics == null)
                _physics = _entity.transform.Find("Physics").gameObject;

            return _physics;
        }
    }
    private GameObject _physics;

    public GameObject Externals
    {
        get
        {
            if (_externals == null)
                _externals = transform.root.Find("Externals").gameObject;

            return _externals;
        }
    }
    private GameObject _externals;

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