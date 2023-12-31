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
        _timeScale = Time.timeScale;

        Time.timeScale = 0;

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
#endif
    }

    private void OnDisable()
    {
        Time.timeScale = _timeScale;

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }
}