using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public TextMeshProUGUI waveText;
    public GameObject levelCompleteText;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner.OnWaveChanged += UpdateWaveText;
        waveSpawner.OnWavesCompleted += LevelComplete;
        levelCompleteText.SetActive(false);
    }

    public void UpdateWaveText(int currentWave, int amountOfWaves)
    {
        waveText.text = "WAVE " + currentWave + "/" + amountOfWaves;
    }

    public void LevelComplete()
    {
        levelCompleteText.SetActive(true);
    }
}
