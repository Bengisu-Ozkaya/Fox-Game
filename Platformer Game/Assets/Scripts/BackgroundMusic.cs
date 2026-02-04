using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public static BackgroundMusic Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BackgroundMusic>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("BackgroundMusic");
                    _instance = go.AddComponent<BackgroundMusic>();
                }
            }
            return _instance;
        }
    }

    private AudioSource audioSource;
    private bool isMusicEnabled = true;
    private bool isInitialized = false;

    private void Awake()
    {
        // Singleton kontrolü
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource'u al
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ÖNEMLÝ: AudioSource'un Time.timeScale'den etkilenmemesi için
        audioSource.ignoreListenerPause = false;

        // Ýlk kez baþlatýlýyorsa
        if (!isInitialized)
        {
            // Müzik durumunu yükle
            isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            isInitialized = true;

            // Müzik durumuna göre baþlat
            if (isMusicEnabled)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    // Müziði tamamen aç/kapa (kullanýcý seçimi)
    public void ToggleMusic(bool enable)
    {
        isMusicEnabled = enable;
        PlayerPrefs.SetInt("MusicEnabled", enable ? 1 : 0);
        PlayerPrefs.Save();

        if (enable)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    // Geçici duraklatma
    public void PauseMusic()
    {
        if (isMusicEnabled && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // Geçici duraklatmayý kaldýr
    public void ResumeMusic()
    {
        if (isMusicEnabled)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }

    public bool IsMusicEnabled()
    {
        return isMusicEnabled;
    }
}