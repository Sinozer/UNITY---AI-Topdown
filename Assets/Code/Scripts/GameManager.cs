// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    public Transform Player => _player;
    [SerializeField] private Transform _player;

    public SOEntityList EntityList => _entityList;
    [SerializeField] private SOEntityList _entityList;
    #endregion Fields
    private void Start()
    {
        FindPlayer();
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindPlayer();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FindPlayer()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null)
            return;

        _player = go.transform;
    }
    public void Quit()
    {
        Application.Quit();
    }
}