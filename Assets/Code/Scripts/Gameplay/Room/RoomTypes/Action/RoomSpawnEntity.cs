// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnEntity : RoomChild
{
    public bool IsEnded => _currentWaveIndex > _waves.Count;

    public List<SOWave> Waves => _waves;
    [SerializeField, InlineEditor] private List<SOWave> _waves;

    public int CurrentWaveIndex
    {
        get => _currentWaveIndex;
        set => _currentWaveIndex = value;
    }

    private int _currentWaveIndex = 0;

    private Coroutine _spawnRoutine;

    public bool IsInFight => _spawnRoutine != null;

    public void SpawnWave()
    {
        _spawnRoutine = StartCoroutine(RoomWaves());

        IEnumerator RoomWaves()
        {
            foreach (var w in _waves)
            {
                yield return w.RunWave(this);
            }

            _spawnRoutine = null;
        }
    }
}