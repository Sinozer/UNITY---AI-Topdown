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
    [SerializeField] private Player _player;
    public Player Player
    {
        get => _player;
        set => _player = value;
    }

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

    private void FindPlayer()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null)
            return;

        _player = go.GetComponent<Player>();
    }
    public void Quit()
    {
        Application.Quit();
    }
}