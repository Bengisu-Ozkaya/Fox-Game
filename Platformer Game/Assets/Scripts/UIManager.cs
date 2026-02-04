using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
    public PlayerControl playerControl;
    [SerializeField] private LevelSkipManager levelSkipManager;
    [SerializeField] private CrankManager crankManager;
    public GameObject climbButton;
    public GameObject skipButton, previousButton;
    public TMP_Text healthText;
    public GameObject pauseButton, pausePanel, menuButton, continueButton, levelButton, pauseRestartButton;
    public GameObject deathPanel, restartButton, deathMenuButton;
    [SerializeField] private GameObject cranckButtonX;
    [SerializeField] private GameObject cranckButtonY;
    bool isPaused = false;

    private void Update()
    {
        healthText.text = "Health: " + playerControl.GetHealth().ToString();

        //tırmanma
        if (playerControl.canClimbing())
        {
            climbButton.SetActive(true);
        }
        else
        {
            climbButton.SetActive(false);
            playerControl.SetClimbDirection(0);
        }

        //level geçme
        if (levelSkipManager.returnSkipLevel())
        {
            skipButton.SetActive(true);
        }
        else
        {
            skipButton.SetActive(false);
        }

        if (levelSkipManager.returnPreviousLevel())
        {
            previousButton.SetActive(true);
        }
        else
        {
            previousButton.SetActive(false);
        }

        // Ölüm veya Düşme kontrolü
        if (playerControl.isDeadStatus() || playerControl.isFallingStatus())
        {
            ShowDeathPanel();
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.PauseMusic();
        }

        // Crank Buton Kontrolü
        if (playerControl.CrankStat())
        {
            if (crankManager.getCrankAxis() == "x")
            {
                cranckButtonX.SetActive(true);
                Debug.Log(" x Crank butonunu aktif ettim");
            }
            else if (crankManager.getCrankAxis() == "y")
            {
                cranckButtonY.SetActive(true);
                Debug.Log("y Crank butonunu aktif ettim");
            }
        }
        else
        {
            if (crankManager.getCrankAxis() == "x")
            {
                cranckButtonX.SetActive(false);
                Debug.Log("x crank butonu artık görünemez");
            }
            else if (crankManager.getCrankAxis() == "y")
            {
                cranckButtonY.SetActive(false);
                Debug.Log("y crank butonu artık görünemez");
            }
        }
    }

    public void PressCrank()
    {
        crankManager.ToggleCrank();
    }

    private void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseButton()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true);
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.PauseMusic();
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            pausePanel.SetActive(false);
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.ResumeMusic();
        }
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        // Pause ekranından menüye dönüyoruz, müziği resume et
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.ResumeMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false);
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.ResumeMusic();
    }

    public void LevelButton()
    {
        Time.timeScale = 1f;
        // Pause ekranından level selection'a gidiyoruz, müziği resume et
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.ResumeMusic();
        SceneManager.LoadScene("LevelSelection");
    }

    public void RestartButton()
    {
        playerControl.restartLevel();
        deathPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    //crank buttonları göster/gizle
    public void ShowCrankButtonX(bool show)
    {
        cranckButtonX.SetActive(show);
    }

    public void ShowCrankButtonY(bool show)
    {
        cranckButtonY.SetActive(show);
    }
}