// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 02/01/24
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimerCanvas : MonoBehaviour
{
    /// <summary>
    /// The text to display the time.
    /// Should be a child of this object.
    /// </summary>
    private TMP_Text _text;

    private Stopwatch _stopwatch;

    /// <summary>
    /// Search for the player's stopwatch.
    /// </summary>
    /// <returns> True if found, false otherwise.</returns>
    private bool SearchForPlayerStopwatch()
    {
        if (PlayerManager.IsInitialized == false)
            return false;

        _stopwatch = PlayerManager.Instance.Stopwatch;

        return _stopwatch != null;
    }

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        if (SearchForPlayerStopwatch() == false)
            return;

        _text.text = _stopwatch.GetTimeString();
    }

    private void OnEnable()
    {
        if (_stopwatch == null)
        {
            if (SearchForPlayerStopwatch() == false)
                return;
        }

        _stopwatch.OnPerform += DisplayTime;
    }

    private void OnDisable()
    {
        if (_stopwatch == null)
            return;

        _stopwatch.OnPerform -= DisplayTime;
    }

    private void DisplayTime()
    {
        _text.text = _stopwatch.GetTimeString();
    }
}