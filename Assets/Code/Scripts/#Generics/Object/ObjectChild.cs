// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 27/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public abstract class ObjectChild<T> : SerializedMonoBehaviour where T : MonoBehaviour
{
    public T Object
    {
        get
        {
            if (_object == null)
                _object = GetComponentInParent<T>(true);

            return _object;
        }
    }
    private T _object;

    #region Managers
    public AudioManager AudioManager
    {
        get
        {
            if (_audioManager == null)
                _audioManager = transform.root.GetComponentInChildren<AudioManager>(true);

            return _audioManager;
        }
    }
    private AudioManager _audioManager;

    public VFXManager VFXManager
    {
        get
        {
            if (_vfxManager == null)
                _vfxManager = transform.root.GetComponentInChildren<VFXManager>(true);

            return _vfxManager;
        }
    }
    private VFXManager _vfxManager;
    #endregion Managers

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
        return Actions.GetComponentInChildren<TAction>(true);
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
                _rigidbody2D = Object.GetComponentInChildren<Rigidbody2D>(true);

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
        return Physics.GetComponentInChildren<TPhysics>(true);
    }
    public TPhysics GetCollider2D<TPhysics>(string name) where TPhysics : Collider2D
    {
        return Physics.transform.Find(name).GetComponentInChildren<TPhysics>(true);
    }

    public TPhysics[] GetColliders2D<TPhysics>() where TPhysics : Collider2D
    {
        return Physics.GetComponentsInChildren<TPhysics>(true);
    }
    public TPhysics[] GetColliders2D<TPhysics>(string name) where TPhysics : Collider2D
    {
        return Physics.transform.Find(name).GetComponentsInChildren<TPhysics>(true);
    }
    #endregion Physics

    #region VFX
    public GameObject VFX
    {
        get
        {
            if (_vfx == null)
                _vfx = Object.transform.Find("VFX")?.gameObject;

            return _vfx;
        }
    }
    private GameObject _vfx;

    public V GetVFX<V>() where V : MonoBehaviour
    {
        return VFX.GetComponentInChildren<V>(true);
    }
    #endregion VFX

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

    public V GetExternal<V>() where V : Component
    {
        return Externals.GetComponentInChildren<V>(true);
    }
    public V GetExternal<V>(string name) where V : Component
    {
        return Externals.transform.Find(name).GetComponentInChildren<V>(true);
    }

    public V[] GetExternalChildrens<V>() where V : Component
    {
        return Externals.GetComponentsInChildren<V>(true);
    }
    public V[] GetExternalChildrens<V>(string name) where V : Component
    {
        return Externals.transform.Find(name).GetComponentsInChildren<V>(true);
    }
    #endregion Externals
}