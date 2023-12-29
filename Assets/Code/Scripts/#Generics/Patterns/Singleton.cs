// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    [SerializeField, Tooltip("If true, the object will not be destroyed on scene load.")] private bool _dontDestroyOnLoad = true;
    [SerializeField, Tooltip("If true, the object will replace the previous one if it exists.")] private bool _replacePrevious = false;

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<T>();

            if (_instance != null)
                return _instance;

            GameObject go = new(typeof(T).Name);
            _instance = go.AddComponent<T>();

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            return;
        }

        if (_replacePrevious == false)
        {
            if (_instance == this)
                return;

            Destroy(gameObject);
            return;
        }

        Destroy(_instance.gameObject);
        _instance = this as T;

        if (_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}