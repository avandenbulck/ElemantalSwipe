using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public EnemySpawnInfo[] enemySpawns;

    public event Action OnWaveComplete = delegate { };

    int enemiesLeft;
    int enemySpawnIndex;
    bool spawnWhenEnemiesCleared;
    List<GameObject> indestructableObjects;

    void Start()
    {
        enemySpawnIndex = 0;
        indestructableObjects = new List<GameObject>();
        spawnWhenEnemiesCleared = false;
        SpawnNextEnemy();
    }

    public void SpawnNextEnemy()
    {
        if(!HaveSpawnedAllEnemies())
        {
            EnemySpawnInfo enemySpawnInfo = enemySpawns[enemySpawnIndex];
            enemySpawnIndex++;
            GameObject enemyObject = Instantiate(enemySpawnInfo.enemyPrefab, enemySpawnInfo.position.position, Quaternion.identity);

            if(enemySpawnInfo.spawnSeqType != SpawnSequenceType.Indestructable)
            {
                enemiesLeft += 1;
                Enemy enemy = enemyObject.GetComponentInChildren<Enemy>();
                enemy.OnDeath.AddListener(EnemyDied);
            }
            else
            {
                indestructableObjects.Add(enemyObject);
            }

            if(enemySpawnIndex < enemySpawns.Length)
            {
                EnemySpawnInfo nextEnemySpawnInfo = enemySpawns[enemySpawnIndex];
                switch (nextEnemySpawnInfo.spawnSeqType)
                {
                    case SpawnSequenceType.PreviousEnemiesCleared:
                        spawnWhenEnemiesCleared = true;
                        break;
                    case SpawnSequenceType.Immediate:
                        SpawnNextEnemy();
                        break;
                    case SpawnSequenceType.Indestructable:
                        SpawnNextEnemy();
                        break;
                    default:
                        break;
                }
            }
            
        } 
    }

    void EnemyDied()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            if(!HaveSpawnedAllEnemies())
            {
                if (spawnWhenEnemiesCleared)
                {
                    spawnWhenEnemiesCleared = false;
                    SpawnNextEnemy();
                }              
            } else
            {
                foreach (GameObject indestructableObject in indestructableObjects)
                {
                    Destroy(indestructableObject);
                }
                OnWaveComplete.Invoke();
                Destroy(gameObject);
            }         
        }      
    }

    private Boolean HaveSpawnedAllEnemies()
    {
        return enemySpawnIndex >= enemySpawns.Length;
    }
}

[Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public Transform position;
    public SpawnSequenceType spawnSeqType;
}

public enum SpawnSequenceType
{
    PreviousEnemiesCleared,
    Immediate,
    Indestructable
}
