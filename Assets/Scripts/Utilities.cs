using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Utilities : MonoBehaviour
{
    [SerializeField] Slider gravSlider;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] FloatVariable gravity;
    [SerializeField] BoolVariable gamePause;
    [SerializeField] TMP_Text gravDisplay;

    private void Awake()
    {
        gravSlider.value = gravity.value;
        gravDisplay.text = (Mathf.Round(gravity.value * 10) / 10).ToString();
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void SetGravity()
    {
        gravity.value = gravSlider.value;
        gravDisplay.text = (Mathf.Round(gravity.value * 10) / 10).ToString();
    }

    public void PauseSim()
    {
        pauseButton.SetActive(false);
        gamePause.value = true;
        resumeButton.SetActive(true);
    }

    public void ResumeSim()
    {
        pauseButton.SetActive(true);
        gamePause.value = false;
        resumeButton.SetActive(false);
    }
}
