using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class audioManager : MonoBehaviour
{
    public audioManager instance;

    [Header("Background Music")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    [Range(0.0001f, 1f)]
    public float bgmSoundVolume;
    public AudioClip menuClip;
    [Range(0.0001f, 1f)]
    public float menuSoundVolume;

    [Header("Sound FX")]
    public AudioSource sfxSource;

    [Header("Shooting Sounds")]
    public AudioClip[] shootSounds; // Wanneer de speler schiet
    [Range(0.0001f, 1f)]
    public float shootSoundVolume;
    public AudioClip beamSound; // Beam Sound van speler
    [Range(0.0001f, 1f)]
    public float beamSoundVolume;
    public AudioClip beamCharged; // Geluid wanneer beam weer geschoten kan worden
    [Range(0.0001f, 1f)]
    public float chargedSoundVolume;

    [Header("Hit Sounds")]
    public AudioClip enemyHitSound; // Wanneer een enemy word gehit
    [Range(0.0001f, 1f)]
    public float enemyhitSoundVolume;
    public AudioClip[] playerHitSounds;

    // Wanneer de speler word gehit
    [Range(0.0001f, 1f)]
    public float playerhitSoundVolume;

    [Header("Menu Sounds")]
    public AudioClip menuClickSound; // Wanneer je iets in de menu klikt
    [Range(0.0001f, 1f)]
    public float menuclickSoundVolume;
    public AudioClip menuHoverSound; // Wanneer je over iets hovert in een menu
    [Range(0.0001f, 1f)]
    public float menuhoverSoundVolume;

    [Header("Death Sounds")]
    // Deze zijn wel duidelijk wat ze doen denk ik
    public AudioClip playerDeath;
    [Range(0.0001f, 1f)]
    public float playerdeathSoundVolume;
    public AudioClip enemyDeath;
    [Range(0.0001f, 1f)]
    public float enemydeathSoundVolume;

    [Header("Victory/Loss Sounds")]
    public AudioClip PlayerVictory;
    [Range(0.0001f, 1f)]
    public float playervictorySoundVolume;
    public AudioClip PlayerLoss;
    [Range(0.0001f, 1f)]
    public float playerlossSoundVolume;

    [Header("Mixer")]
    public AudioMixer mixer;

    public bool click;
    public bool hover;

    private void Start()
    {
        StopAllSounds();
    }

    void Awake()
    {
        Debug.Log("awake");
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Zorgt ervoor dat de muziek door kan spelen na de scene change naar de bossfight
        }
        else { Destroy(gameObject);  } // Zorgt ervoor dat er niet 2 audioManagers zijn want anders kan dat breken
    }

    public void StopAllSounds()
    {
        string scene = SceneManager.GetActiveScene().name;

        bgmSource.Stop();
        sfxSource.Stop();

        Debug.Log(scene);

        if (scene == "MainScene 2")
        {
            StartGameMusic();
        }
        if (scene == "Start")
        {
            StartMenuMusic();
        }
        if (scene == "Retry")
        {
            PlayLossSound();
        }
        if (scene == "Win")
        {
            PlayVictorySound();
        }
    }

    public void StartGameMusic()
    {
        bgmSource.Stop();
        mixer.SetFloat("Music Volume", Mathf.Log10(bgmSoundVolume) * 20);
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void StartMenuMusic()
    {
        bgmSource.Stop();
        mixer.SetFloat("Music Volume", Mathf.Log10(menuSoundVolume) * 20);
        bgmSource.clip = menuClip;
        bgmSource.Play();
    }

    // Shooting sounds
    public void PlayShootSound() 
    {
        if (shootSounds.Length == 0) return;

        int rndHitSound = Random.Range(0, shootSounds.Length);
        mixer.SetFloat("Sound Effects Volume", Mathf.Log10(shootSoundVolume) * 20); sfxSource.PlayOneShot(shootSounds[rndHitSound]); 
    }
    public void PlayBeamSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(beamSoundVolume) * 20); sfxSource.PlayOneShot(beamSound); }
    public void PlayChargedSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(chargedSoundVolume) * 20); sfxSource.PlayOneShot(beamCharged); }

    // Hit sounds
    public void PlayEnemyHitSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(enemyhitSoundVolume) * 20); sfxSource.PlayOneShot(enemyHitSound); }
    public void PlayPlayerHitSound() 
    {
        if (playerHitSounds.Length == 0) return;

        int rndHitSound = Random.Range(0, playerHitSounds.Length);
        mixer.SetFloat("Sound Effects Volume", Mathf.Log10(playerhitSoundVolume) * 20); sfxSource.PlayOneShot(playerHitSounds[rndHitSound]); 
    }

    // Menu sounds
    public void PlayMenuClickSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(menuclickSoundVolume) * 20); sfxSource.PlayOneShot(menuClickSound); }
    public void PlayHoverSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(menuhoverSoundVolume) * 20); sfxSource.PlayOneShot(menuHoverSound); }


    // Death sounds
    public void PlayPlayerDeathSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(playerdeathSoundVolume) * 20); sfxSource.PlayOneShot(playerDeath); }
    public void PlayEnemyDeathSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(enemydeathSoundVolume) * 20); sfxSource.PlayOneShot(enemyDeath); }

    // Victory/loss sounds
    public void PlayVictorySound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(playervictorySoundVolume) * 20); sfxSource.PlayOneShot(PlayerVictory); }
    public void PlayLossSound() { mixer.SetFloat("Sound Effects Volume", Mathf.Log10(playerlossSoundVolume) * 20); sfxSource.PlayOneShot(PlayerLoss); }
}