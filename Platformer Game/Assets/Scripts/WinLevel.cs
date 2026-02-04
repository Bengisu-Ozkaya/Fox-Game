using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject leveltButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject soundOnButton, soundOffButton;
    bool isClicked = false;

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
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
        SceneManager.LoadScene("LevelSelection");
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