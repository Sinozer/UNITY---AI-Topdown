// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 12/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;

public class SceneManager : Singleton<SceneManager>
{
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void LoadSceneAdditive(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
    }

    public void UnloadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneIndex);
    }

    public UnityEngine.SceneManagement.Scene GetCurrentScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }
}