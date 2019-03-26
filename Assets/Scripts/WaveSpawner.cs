using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public int numberOfEnemies;
    public float timeBetweenEnemies;
    public List<Transform> positionsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnWave()
    {
        int numberOfEnemiesSpawned = 0;
        while(numberOfEnemiesSpawned < numberOfEnemies)
        {
            int random = Random.Range(0, positionsToSpawn.Count);
            Debug.Log(random);
            Transform positionToSpawn = positionsToSpawn[random];
            positionsToSpawn.RemoveAt(random);
            Instantiate(enemyToSpawn, positionToSpawn.position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenEnemies);
            numberOfEnemiesSpawned++;
        }
    }
}
