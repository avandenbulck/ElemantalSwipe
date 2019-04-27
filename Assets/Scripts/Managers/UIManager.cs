using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dependencies")]
    public TextMeshProUGUI waveStatusText;
    public GameObject levelCompleteText;
    public TextMeshProUGUI waveStartAndFinishText;
    public TextMeshProUGUI timeText;

    [Header("Timing")]
    public float timeDelayBeforeShowingFirstText;
    public float timeToShowNewWaveText;
    public float timeToShowWaveCompletedText;

    // Start is called before the first frame update
    void Start()
    {
        levelCompleteText.SetActive(false);
        waveStartAndFinishText.text = "";
    }

    public void SetWaveText(int currentWave, int amountOfWaves, Action callback)
    {
        StartCoroutine(ShowWaveText(currentWave, amountOfWaves, callback));  
    }

    public IEnumerator ShowWaveText(int currentWave, int amountOfWaves, Action callback)
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
        callback();
    }

    public void ShowLevelCompleteText()
    {
        levelCompleteText.SetActive(true);
    }

    public void UpdateTime(float time)
    {
        int minutes = (int)Math.Floor(time) / 60;
        int seconds = (int)Math.Floor(time) % 60;

        string text = minutes.ToString("00") + ":" + seconds.ToString("00");
        timeText.text = text;
    }
}
