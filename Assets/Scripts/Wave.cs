using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //public Vector3 targetPos;
    //public float speed;

    public event Action OnWaveComplete;

    public EnemySpawnInfo[] enemySpawns;

    int enemiesLeft;

    int enemySpawnIndex;
    bool spawnWhenEnemiesCleared;
    // Start is called before the first frame update
    void Start()
    {
        //Enemy[] allEnemies = GetComponentsInChildren<Enemy>();
        //enemiesLeft = allEnemies.Length;
        //foreach (Enemy enemy in allEnemies)
        //{
        //    enemy.OnDeath += EnemyDied;
        //}
        enemySpawnIndex = 0;
        spawnWhenEnemiesCleared = false;
        SpawnNextEnemy();
    }

    public void SpawnNextEnemy()
    {
        if(enemySpawnIndex < enemySpawns.Length)
        {
            EnemySpawnInfo enemySpawnInfo = enemySpawns[enemySpawnIndex];
            enemySpawnIndex++;
            GameObject enemyObject = Instantiate(enemySpawnInfo.enemyPrefab, enemySpawnInfo.position.position, Quaternion.identity);
            enemiesLeft += 1;
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            enemy.OnDeath += EnemyDied;
            switch (enemySpawnInfo.spawnSeqType)
            {
                case SpawnSequenceType.PreviousEnemiesCleared:
                    spawnWhenEnemiesCleared = true;
                    break;
                case SpawnSequenceType.Immediate:
                    SpawnNextEnemy();
                    break;
                default:
                    break;
            }
        } 
    }

    void EnemyDied()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            if(enemySpawnIndex < enemySpawns.Length)
            {
                if (spawnWhenEnemiesCleared)
                {
                    spawnWhenEnemiesCleared = false;
                    SpawnNextEnemy();
                }              
            } else
            {
                OnWaveComplete.Invoke();
                Destroy(gameObject);
            }         
        }      
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
    Immediate
}
