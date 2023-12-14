// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 05/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform _player;

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

    public Transform GetPlayer()
    {
        return _player;
    }

    public void Quit()
    {
        Application.Quit();
    }
}