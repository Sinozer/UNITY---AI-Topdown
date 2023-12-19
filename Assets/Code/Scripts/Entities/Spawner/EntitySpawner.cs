// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public bool IsEnded => _currentWaveIndex > _waves.Count;

    //public enum ESpawnType
    //{
    //    Wave,       // Spawn a wave of enemies, the next wave will spawn when the previous one is defeated
    //    Timed       // Spawn waves of enemies at a timed interval
    //}
    //private ESpawnType _spawnType = ESpawnType.Wave;
    //
    //public float TimeBetweenWaves => _timeBetweenWaves;
    //[SerializeField] private float _timeBetweenWaves;

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
        _spawnRoutine= StartCoroutine(RoomWaves());
        IEnumerator RoomWaves()
        {
            foreach(var w in _waves)
            {
                yield return w.RunWave();
            }
            _spawnRoutine = null;
        }
    }
}