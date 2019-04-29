using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject levelSelectButtons;

    public void OnStartButtonClick()
    {
        startButton.SetActive(false);
        levelSelectButtons.SetActive(true);
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }
}
