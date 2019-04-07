using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] waves;
    public float timeBeforeSpawningAWave;
    public event Action<int, int> OnWaveChanged;
    public event Action OnWavesCompleted;

    int waveIndex;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        OnWaveChanged.Invoke(1, waves.Length);
        StartSpawningWave();
    }

    void StartSpawningWave()
    {
        StartCoroutine(SpawnWave());
    }

    void WaveComplete()
    {
        waveIndex++;
        if (waveIndex >= waves.Length)
            OnWavesCompleted.Invoke();
        else
        {
            StartSpawningWave();
            OnWaveChanged.Invoke(waveIndex + 1, waves.Length);
        }  
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeBeforeSpawningAWave);
        GameObject waveObject = Instantiate(waves[waveIndex]);
        Wave wave = waveObject.GetComponent<Wave>();
        wave.OnWaveComplete += WaveComplete; 
    }

}
