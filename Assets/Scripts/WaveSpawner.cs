using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] waves;
    public float delayBeforeSpawningWave;
    public event Action<int, int> OnWaveChanged;
    public event Action OnWavesCompleted;

    int waveIndex;

    public int CurrentWave()
    {
        return waveIndex + 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        OnWaveChanged.Invoke(1, waves.Length);
    }

    public void SpawnNextWave()
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
            OnWaveChanged.Invoke(waveIndex + 1, waves.Length);
        }  
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(delayBeforeSpawningWave);
        GameObject waveObject = Instantiate(waves[waveIndex]);
        Wave wave = waveObject.GetComponent<Wave>();
        wave.OnWaveComplete += WaveComplete; 
    }

}
