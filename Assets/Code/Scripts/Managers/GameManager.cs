// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //[ShowInInspector, InlineEditor]
    [Title("Game Manager", "Game Manager class related inspector", TitleAlignments.Centered)]
    [SerializeField, InlineEditor] private CustomBlackboard _blackboard;
    public CustomBlackboard Blackboard { get => _blackboard; private set => _blackboard = value; }

    protected override void Awake()
    {
        base.Awake();

        if (Blackboard == null)
        {
            Blackboard = ScriptableObject.CreateInstance<CustomBlackboard>();
            return;
        }

        Blackboard = Instantiate(Blackboard);
    }

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
            if (Blackboard.TryFind<Player>("Player", out Player player))
                return player;

            //throw new Exception("Player not found in the blackboard");
            return null;
        }
    }

    public SOEntityList EntityList
    {
        get
        {
            if (Blackboard.TryFind<SOEntityList>("EntityList", out SOEntityList entityList))
                return entityList;

            throw new Exception("EntityList not found in the blackboard");
        }
    }

    public SOProjectileList ProjectileList
    {
        get
        {
            if (Blackboard.TryFind<SOProjectileList>("ProjectileList", out SOProjectileList projectileList))
                return projectileList;

            throw new Exception("ProjectileList not found in the blackboard");
        }
    }

    public int MainMenuSceneIndex
    {
        get
        {
            if (Blackboard.TryFind("MainMenuSceneIndex", out int index))
                return index;

            throw new Exception("MainSceneIndex not found in the blackboard");
        }
    }
    public int GameSceneIndex
    {
        get
        {
            if (Blackboard.TryFind("GameSceneIndex", out int index))
                return index;

            throw new Exception("GameSceneIndex not found in the blackboard");
        }
    }
    public int PauseSceneIndex
    {
        get
        {
            if (Blackboard.TryFind("PauseSceneIndex", out int index))
                return index;

            throw new Exception("PauseSceneIndex not found in the blackboard");
        }
    }
    #endregion Fields

    public void Play()
    {
        CurrentGameState = GameState.Game;
        OnGameStart?.Invoke();

        SceneManager.Instance.LoadScene(GameSceneIndex);

        PlayerManager.Instance.Stopwatch.StartTime();
    }

    public void Pause()
    {
        CurrentGameState = GameState.Pause;
        OnGamePause?.Invoke();

        MenuManager.Instance.IsMenuOpen = true;

        Player.Brain.enabled = false;
    }

    public void Resume()
    {
        CurrentGameState = GameState.Game;
        OnGameResume?.Invoke();

        MenuManager.Instance.IsMenuOpen = false;

        Player.Brain.enabled = true;
    }

    public void End()
    {
        CurrentGameState = GameState.MainMenu;
        OnGameEnd?.Invoke();

        SceneManager.Instance.LoadScene(MainMenuSceneIndex);
    }

    public void Quit()
    {
        // Make things before closing the game

        Application.Quit();
    }
}