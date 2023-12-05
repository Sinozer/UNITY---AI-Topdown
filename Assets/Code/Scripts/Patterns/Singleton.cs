// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Author: Théo
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
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
            DontDestroyOnLoad(gameObject);
        }

        else if (_instance != this)
            Destroy(gameObject);
    }
}