using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] waves;
    public float timeToSpawnWave;

    int waveIndex;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        StartWaveSpawn();
    }

    void StartWaveSpawn()
    {
        StartCoroutine(SpawnWave());
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeToSpawnWave);
        GameObject waveObject = Instantiate(waves[waveIndex]);
        Wave wave = waveObject.GetComponent<Wave>();
        wave.OnWaveComplete += StartWaveSpawn;

        waveIndex++;
        if (waveIndex >= waves.Length)
            waveIndex = 0;
    }
}
