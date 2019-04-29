using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]   
    public UIManager uiManager;

    float time;
    bool gameRunning;
    WaveSpawner waveSpawner;

    private void Awake()
    {
        waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {      
        waveSpawner.OnWaveChanged += WaveChanged;
        waveSpawner.OnWavesCompleted += LevelComplete;
        time = 0;
        uiManager.UpdateTime(time);
        gameRunning = false;
    }

    void Update()
    {
        if (gameRunning)
        {
            time += Time.deltaTime;
            uiManager.UpdateTime(time); 
        }
    }

    private void WaveChanged(int currentWave, int amountOfWaves)
    {
        uiManager.SetWaveText(currentWave, amountOfWaves, WaveTextShown);      
    }

    public void WaveTextShown()
    {
        if(waveSpawner.CurrentWave() == 1)
        {
            gameRunning = true;
        }
        waveSpawner.SpawnNextWave();
    }

    private void LevelComplete()
    {
        gameRunning = false;
        uiManager.ShowLevelCompleteText(time);
    }
}
