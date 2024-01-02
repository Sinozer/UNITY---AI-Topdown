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
    public event Action OnPerform;
    public event Action OnStop;
    public event Action OnReset;
    #endregion Events

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

    /// <summary>
    /// Get the time as a string.
    /// </summary>
    /// <param name="format"> The format to use. </param>
    /// <returns> The time as a string formatted. </returns>
    public string GetTimeString(string format = "mm\\:ss\\.ff")
    {
        return TimeSpan.ToString(format);
    }

    private void Update()
    {
        if (_isRunning == false)
            return;

        _time += UnityEngine.Time.deltaTime;

        OnPerform?.Invoke();
    }
}