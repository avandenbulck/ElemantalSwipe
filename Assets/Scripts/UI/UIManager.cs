using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dependencies")]
    public WaveSpawner waveSpawner;
    public TextMeshProUGUI waveStatusText;
    public GameObject levelCompleteText;
    public TextMeshProUGUI waveStartAndFinishText;

    [Header("Timing")]
    public float timeDelayBeforeShowingFirstText;
    public float timeToShowNewWaveText;
    public float timeToShowWaveCompletedText;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner.OnWaveChanged += WaveChanged;
        waveSpawner.OnWavesCompleted += LevelComplete;
        levelCompleteText.SetActive(false);
        waveStartAndFinishText.text = "";
    }

    public void WaveChanged(int currentWave, int amountOfWaves)
    {
        StartCoroutine(ShowWaveText(currentWave,amountOfWaves));  
    }

    public IEnumerator ShowWaveText(int currentWave, int amountOfWaves)
    {
        yield return new WaitForSeconds(timeDelayBeforeShowingFirstText);

        if (currentWave != 1)
        {
            waveStartAndFinishText.text = "WAVE COMPLETED";
            yield return new WaitForSeconds(timeToShowWaveCompletedText);
        }
        
        waveStartAndFinishText.text = "WAVE " + currentWave;
        waveStatusText.text = "WAVE " + currentWave + "/" + amountOfWaves;
        yield return new WaitForSeconds(timeToShowWaveCompletedText);

        waveStartAndFinishText.text = "";
        waveSpawner.SpawnNextWave();
    }

    public void LevelComplete()
    {
        levelCompleteText.SetActive(true);
    }
}
