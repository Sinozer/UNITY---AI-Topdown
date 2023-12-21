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
    [Serializable]
    public class EnemyType
    {
        public Enemy Entity => _entity;
        [SerializeField] private Enemy _entity;

        public int NumberOfEnemies => _numberOfEnemies;
        [SerializeField] private int _numberOfEnemies;
    }

    public List<EnemyType> Enemies => _enemies;
    [SerializeField] private List<EnemyType> _enemies;

    [SerializeField] private float _timeBetweenSpawns;

    [SerializeField] private float _timeBetweenEnemyTypes;

    public IEnumerator RunWave(EntitySpawner spawner)
    {
        int totalOfEnemies = 0;

        // Instantiate
        foreach (var enemy in Enemies)
        {
            for (int i = 0; i < enemy.NumberOfEnemies; i++)
            {
                // Spawn enemy
                Enemy instance = Instantiate(enemy.Entity, Vector3.zero, Quaternion.identity);
                instance.transform.position = spawner.GetRandomPositionInRoom();

                totalOfEnemies++;

                instance.OnDeath += ManageDeath;
                void ManageDeath()
                {
                    totalOfEnemies--;
                    instance.OnDeath -= ManageDeath;
                }

                // Wait for next enemy
                yield return new WaitForSeconds(_timeBetweenSpawns);
            }

            // Wait for next enemy type
            yield return new WaitForSeconds(_timeBetweenEnemyTypes);
        }

        // Pending
        while (totalOfEnemies > 0)
        {
            yield return null;
        }

        yield break;
    }
}