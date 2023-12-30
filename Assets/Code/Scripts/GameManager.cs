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
        get
        {
            if (_player == null)
                _player = FindObjectOfType<Player>();

            return _player;
        }
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

        Debug.Log("Pause");

        SceneManager.Instance.LoadSceneAdditive(_pauseSceneIndex);

        Debug.Log("Before");
        Player.Brain.enabled = false;
        Debug.Log("After");
        PlayerManager.Instance.Stopwatch.StopTime();
    }

    public void Resume()
    {
        CurrentGameState = GameState.Game;
        OnGameResume?.Invoke();

        Debug.Log("Resume");

        SceneManager.Instance.UnloadScene(_pauseSceneIndex);

        Debug.Log("Before");
        Player.Brain.enabled = true;
        Debug.Log("After");
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