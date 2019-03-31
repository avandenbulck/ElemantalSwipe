using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public TextMeshProUGUI waveText;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawner.OnWaveChanged += UpdateText;
    }

    public void UpdateText(int currentWave, int amountOfWaves)
    {
        waveText.text = "WAVE " + currentWave + "/" + amountOfWaves;
    }
}
