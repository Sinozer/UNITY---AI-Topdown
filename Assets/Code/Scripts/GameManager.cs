// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _mainMenuSceneIndex = 0;
    [SerializeField] private int _gameSceneIndex = 1;
    [SerializeField] private int _pauseSceneIndex = 2;

    #region Events
    public event Action OnGameStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnGameEnd;

    public event Action OnGameStateChange;
    #endregion Events

    #region GameState
    public enum GameState
    {
        MainMenu,
        Game,
        Pause,
        End
    }
    public GameState CurrentGameState
    {
        get => _currentGameState;
        private set
        {
            _currentGameState = value;

            OnGameStateChange?.Invoke();
        }
    }
    [SerializeField] private GameState _currentGameState = GameState.MainMenu;
    #endregion GameState

    #region Fields
    public Player Player
    {
        get => _player;
        set => _player = value;
    }
    [SerializeField] private Player _player;

    public SOEntityList EntityList => _entityList;
    [SerializeField] private SOEntityList _entityList;

    public SOProjectileList ProjectileList => _projectileList;
    [SerializeField] private SOProjectileList _projectileList;

    public GameObject Projectile => _projectile;
    [SerializeField] private GameObject _projectile;
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

    public void Play()
    {
        CurrentGameState = GameState.Game;
        OnGameStart?.Invoke();

        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameSceneIndex);

        PlayerManager.Instance.Stopwatch.StartTime();
    }

    public void Pause()
    {
        CurrentGameState = GameState.Pause;
        OnGamePause?.Invoke();

        SceneManager.Instance.LoadSceneAdditive(_pauseSceneIndex);

        Player.Brain.enabled = false;
        PlayerManager.Instance.Stopwatch.StopTime();
    }

    public void Resume()
    {
        CurrentGameState = GameState.Game;
        OnGameResume?.Invoke();

        SceneManager.Instance.UnloadScene(_pauseSceneIndex);

        Player.Brain.enabled = true;
        PlayerManager.Instance.Stopwatch.StartTime();
    }

    public void End()
    {
        CurrentGameState = GameState.MainMenu;
        OnGameEnd?.Invoke();

        SceneManager.Instance.LoadScene(_mainMenuSceneIndex);
    }
    
    public void Quit()
    {
        // Make things before closing the game

        Application.Quit();
    }
}