using UnityEngine;

public class MenuScene : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject levelButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject soundOnButton, soundoffButton;
    bool isClicked = false;


    private void Start()
    {
        // Menüye döndüðümüzde müziði devam ettir
        if (BackgroundMusic.Instance != null && BackgroundMusic.Instance.IsMusicEnabled())
        {
            BackgroundMusic.Instance.ResumeMusic();
        }

        // Müzik durumunu kontrol et ve butonlarý güncelle
        if (BackgroundMusic.Instance != null)
        {
            bool musicEnabled = BackgroundMusic.Instance.IsMusicEnabled();
            UpdateSoundButtons(musicEnabled);
        }
    }

    // Ses butonlarýnýn görünümünü güncelle
    private void UpdateSoundButtons(bool musicEnabled)
    {
        if (soundOnButton != null && soundoffButton != null)
        {
            soundOnButton.SetActive(!musicEnabled);
            soundoffButton.SetActive(musicEnabled);
        }
    }
    public void PlayButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OptionsButton()
    {
        if (!isClicked)
        {
            isClicked = true;
            optionsPanel.SetActive(true);
        }
        else
        {
            isClicked = false;
            optionsPanel.SetActive(false);
        }
    }

    public void LevelButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }

    public void SoundOn()
    {
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.ToggleMusic(true);
    }

    public void SoundOff()
    {
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.ToggleMusic(false);
    }
}