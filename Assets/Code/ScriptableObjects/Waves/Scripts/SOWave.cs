// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 18/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class SOWave : SerializedScriptableObject
{
    [System.Serializable]
    public class EnemyType
    {
        public Enemy Entity => _entity;
        [SerializeField] private Enemy _entity;

        public int NumberOfEnemies => _numberOfEnemies;
        [SerializeField] private int _numberOfEnemies;
    }

    public List<EnemyType> Enemies => _enemies;
    [SerializeField] private List<EnemyType> _enemies;

    public bool IsSpawned => _isSpawned;
    private bool _isSpawned = false;

    [SerializeField] private float _timeBetweenSpawns;

    [SerializeField] private float _timeBetweenEnemyTypes;

    public event Action<Enemy> PrepareNewSpawnType;

    public IEnumerator RunWave(MonoBehaviour mb)
    {
        int _totalOfEnemies = 0;

        // Instantiate
        foreach (var enemy in Enemies)
        {
            Enemy instance = null;
            PrepareNewSpawnType?.Invoke(instance);
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < enemy.NumberOfEnemies; i++)
            {
                // Spawn enemy
                instance = GameObject.Instantiate(enemy.Entity, Vector3.zero, Quaternion.identity);

                _totalOfEnemies++;

                instance.OnDeath += ManageDeath;
                void ManageDeath()
                {
                    _totalOfEnemies--;
                    instance.OnDeath -= ManageDeath;
                }

                // Wait for next enemy
                yield return new WaitForSeconds(_timeBetweenSpawns);
            }

            // Wait for next enemy type
            yield return new WaitForSeconds(_timeBetweenEnemyTypes);
        }

        // Pending
        while(_totalOfEnemies > 0)
        {
            yield return null;
        }

        yield break;
    }
}