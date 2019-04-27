using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public WaveSpawner waveSpawner;
    public UIManager uiManager;

    int time;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner.OnWaveChanged += WaveChanged;
        waveSpawner.OnWavesCompleted += LevelComplete;
    }

    private void LevelComplete()
    {
        uiManager.ShowLevelCompleteText();
    }

    private void WaveChanged(int currentWave, int amountOfWaves)
    {
        uiManager.SetWaveText(currentWave, amountOfWaves, WaveTextShown);      
    }

    public void WaveTextShown()
    {
        waveSpawner.SpawnNextWave();
    }
}
