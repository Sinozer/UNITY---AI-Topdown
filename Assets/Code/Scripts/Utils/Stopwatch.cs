// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 29/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System;
using TMPro;
using UnityEngine;

/// <summary>
/// A simple stopwatch class to measure time.
/// </summary>
public class Stopwatch : MonoBehaviour
{
    #region Events
    public event Action OnStart;
    public event Action OnStop;
    public event Action OnReset;
    #endregion Events

    public TMP_Text Text
    {
        get
        {
            if (_text == null)
            {
                // Find object in scene by name.
                GameObject go = GameObject.Find("TimerCanvas");

                if (go == null)
                    return null;

                _text = go.GetComponentInChildren<TMP_Text>();
            }

            return _text;
        }
        set => _text = value;
    }
    [SerializeField] private TMP_Text _text;

    public float Time => _time;
    private float _time = 0f;

    public TimeSpan TimeSpan => TimeSpan.FromSeconds(_time);

    public bool IsRunning => _isRunning;
    private bool _isRunning = false;

    /// <summary>
    /// Start the stopwatch.
    /// </summary>
    /// <returns>The time when started.</returns>
    public float StartTime()
    {
        OnStart?.Invoke();

        _isRunning = true;
        return _time;
    }

    /// <summary>
    /// Stop the stopwatch.
    /// </summary>
    /// <returns>The time elapsed.</returns>
    /// <exception cref="UnityException">Thrown when the stopwatch is not started.</exception>
    public float StopTime()
    {
        if (_isRunning == false)
        {
            //throw new UnityException("The stopwatch is not started.");

            Debug.LogWarning("The stopwatch is not started.");
            return _time;
        }

        OnStop?.Invoke();

        _isRunning = false;
        return _time;
    }

    /// <summary>
    /// Reset the stopwatch.
    /// </summary>
    public void ResetTime()
    {
        OnReset?.Invoke();

        _time = 0f;
    }

    public string GetTimeString(string format = "mm\\:ss\\.ff")
    {
        return TimeSpan.ToString(format);
    }

    public void SetTimerToText()
    {
        if (Text == null)
            return;

        Text.text = GetTimeString();
    }

    private void Update()
    {
        if (_isRunning == false)
            return;

        _time += UnityEngine.Time.deltaTime;

        SetTimerToText();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        SetTimerToText();
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}