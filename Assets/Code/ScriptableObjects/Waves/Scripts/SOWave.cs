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
                Enemy instance = Instantiate(enemy.Entity, spawner.GetRandomPositionInRoom(), Quaternion.identity);

                int spawnPointIndex = UnityEngine.Random.Range(0, spawner.PatrolAreas.Count);

                for (int j = 0; j < spawner.PatrolAreas.Count; j++)
                {
                    // Get a random point in the patrol area which is a capsule collider 2D
                    var bounds = spawner.PatrolAreas[j].bounds;
                    var x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
                    var y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
                    var value = new Vector3(x, y, 9);
                    CustomPatrol patrol = instance.GetComponentInChildren<CustomPatrol>();
                    patrol.Waypoints.Add(value);

                    if (j != spawnPointIndex)
                        continue;

                    instance.transform.position = value;
                    patrol.Index = spawnPointIndex;
                }

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