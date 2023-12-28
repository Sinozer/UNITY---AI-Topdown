// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 27/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public abstract class ObjectChild<T> : MonoBehaviour where T : MonoBehaviour
{
    public T Object
    {
        get
        {
            if (_object == null)
                _object = GetComponentInParent<T>();

            return _object;
        }
    }
    private T _object;

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

    #region Actions
    public GameObject Actions
    {
        get
        {
            if (_actions == null)
                _actions = Object.transform.Find("Actions").gameObject;

            return _actions;
        }
    }
    private GameObject _actions;
    public TAction GetAction<TAction>() where TAction : MonoBehaviour
    {
        return Actions.GetComponentInChildren<TAction>();
    }
    #endregion Actions

    #region Render
    public GameObject Render
    {
        get
        {
            if (_render == null)
                _render = Object.transform.Find("Render").gameObject;

            return _render;
        }
    }
    private GameObject _render;

    public Transform GetRender(string name)
    {
        return Render.transform.Find(name);
    }

    public Animator GetRenderAnimator(string name)
    {
        Animator animator = null;
        Transform render = Render.transform.Find(name);

        if (render != null)
            animator = render.GetComponent<Animator>();

        return animator;
    }
    #endregion Render

    #region Physics
    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (_rigidbody2D == null)
                _rigidbody2D = Object.GetComponentInChildren<Rigidbody2D>();

            return _rigidbody2D;
        }
    }
    private Rigidbody2D _rigidbody2D;

    public GameObject Physics
    {
        get
        {
            if (_physics == null)
                _physics = Object.transform.Find("Physics").gameObject;

            return _physics;
        }
    }
    private GameObject _physics;

    public TPhysics GetCollider2D<TPhysics>() where TPhysics : Collider2D
    {
        return Physics.GetComponentInChildren<TPhysics>();
    }
    public TPhysics GetCollider2D<TPhysics>(string name) where TPhysics : Collider2D
    {
        return Physics.transform.Find(name).GetComponentInChildren<TPhysics>();
    }

    public TPhysics[] GetColliders2D<TPhysics>() where TPhysics : Collider2D
    {
        return Physics.GetComponentsInChildren<TPhysics>();
    }
    public TPhysics[] GetColliders2D<TPhysics>(string name) where TPhysics : Collider2D
    {
        return Physics.transform.Find(name).GetComponentsInChildren<TPhysics>();
    }
    #endregion Physics

    #region Externals
    public GameObject Externals
    {
        get
        {
            if (_externals == null)
                _externals = Object.transform.Find("Externals").gameObject;

            return _externals;
        }
    }
    private GameObject _externals;

    public Transform GetExternal(string name)
    {
        return Externals.transform.Find(name);
    }

    public Transform[] GetExternalChildrens(string name)
    {
        return Externals.transform.Find(name).GetComponentsInChildren<Transform>();
    }
    #endregion Externals
}