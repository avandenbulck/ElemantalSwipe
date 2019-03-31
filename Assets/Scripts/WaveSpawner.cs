﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] waves;
    public float timeToSpawnWave;
    public event Action<int, int> OnWaveChanged;

    int waveIndex;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        OnWaveChanged.Invoke(1, waves.Length);
        StartWaveSpawn();
    }

    void StartWaveSpawn()
    {
        StartCoroutine(SpawnWave());
    }

    void WaveComplete()
    {
        waveIndex++;
        if (waveIndex >= waves.Length)
            waveIndex = 0;

        StartWaveSpawn();
        OnWaveChanged.Invoke(waveIndex + 1, waves.Length);
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeToSpawnWave);
        GameObject waveObject = Instantiate(waves[waveIndex]);
        Wave wave = waveObject.GetComponent<Wave>();
        wave.OnWaveComplete += WaveComplete;
        
    }


}
