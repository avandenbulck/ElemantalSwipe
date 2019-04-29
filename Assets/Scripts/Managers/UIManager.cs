using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Dependencies")]
    public TextMeshProUGUI waveStatusText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI waveStartAndFinishText;
    public TextMeshProUGUI timeText;
    public GameObject backToMainMenuButton;

    [Header("Timing")]
    public float timeDelayBeforeShowingFirstText;
    public float timeToShowNewWaveText;
    public float timeToShowWaveCompletedText;

    // Start is called before the first frame update
    void Start()
    {
        levelCompleteText.text = "";
        backToMainMenuButton.SetActive(false);
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

    public void ShowLevelCompleteText(float time)
    {
        levelCompleteText.text = "LEVEL COMPLETE\n\n" + "TIME\n" + GetTimeString(time);
        backToMainMenuButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateTime(float time)
    {
        timeText.text = GetTimeString(time);
    }

    public string GetTimeString(float time)
    {
        int minutes = (int)Math.Floor(time) / 60;
        int seconds = (int)Math.Floor(time) % 60;

        string timeString = minutes.ToString("00") + ":" + seconds.ToString("00");
        return timeString;
    }
}
