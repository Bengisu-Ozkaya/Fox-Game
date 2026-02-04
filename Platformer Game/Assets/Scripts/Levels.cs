using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject level1Button, level2Button, level3Button, level4Button, level5Button;
    [SerializeField] private GameObject menuButton;


    private void Start()
    {
        // Level selection'a geldiðimizde müziði devam ettir
        if (BackgroundMusic.Instance != null && BackgroundMusic.Instance.IsMusicEnabled())
        {
            BackgroundMusic.Instance.ResumeMusic();
        }
    }
    public void Level1Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void Level2Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void Level3Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }

    public void Level4Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level4");
    }

    public void Level5Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level5");
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}