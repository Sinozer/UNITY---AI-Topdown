// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 30/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    private float _timeScale = 1;

    private void OnEnable()
    {
        if (MenuManager.IsInitialized == false)
            return;

        MenuManager.Instance.OnMenuOpen += OnMenuOpen;
        MenuManager.Instance.OnMenuClose += OnMenuClose;
    }

    private void OnDisable()
    {
        Time.timeScale = _timeScale;

        if (MenuManager.IsInitialized == false)
            return;

        MenuManager.Instance.OnMenuOpen -= OnMenuOpen;
        MenuManager.Instance.OnMenuClose -= OnMenuClose;
    }

    private void OnMenuOpen(string name)
    {
        if (name != "Pause")
            return;

        _timeScale = Time.timeScale;

        Time.timeScale = 0;

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
#endif
    }

    private void OnMenuClose(string name)
    {
        if (name != "Pause")
            return;

        Time.timeScale = _timeScale;

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }
}