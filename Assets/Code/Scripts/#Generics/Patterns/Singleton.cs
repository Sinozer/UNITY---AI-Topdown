// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    [Title("Singleton", "Singleton class related inspector", TitleAlignments.Centered)]
    [SerializeField, Tooltip("If true, the object will not be destroyed on scene load.")] private bool _dontDestroyOnLoad = false;
    [SerializeField, Tooltip("If true, the object will replace the previous one if it exists.")] private bool _replacePrevious = false;

    public static bool IsInitialized => _instance != null;

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

        if (_replacePrevious == true)
        {
            Destroy(_instance.gameObject);
            _instance = this as T;

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            return;
        }

        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}