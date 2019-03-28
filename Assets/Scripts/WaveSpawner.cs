using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wavePrefab;
    public float timeToSpawnWave;

    // Start is called before the first frame update
    void Start()
    {
        StartWaveSpawn();
    }

    void StartWaveSpawn()
    {
        StartCoroutine(SpawnWave());
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeToSpawnWave);
        GameObject waveObject = Instantiate(wavePrefab);
        Wave wave = waveObject.GetComponent<Wave>();
        wave.OnWaveComplete += StartWaveSpawn;
    }
}
