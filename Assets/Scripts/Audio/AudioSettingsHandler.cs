using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsHandler : MonoBehaviour
{
    public static AudioSettingsHandler Instance {  get; private set; }
    public bool enableMusic;
    public bool enableSound;

    [Header("Audio Settings UI Elements")]
    [SerializeField] Image musicButtonImg;
    [SerializeField] Image soundButtonImg;

    [Space(20f)]
    [Header("Audio Sprite Assets")]
    [SerializeField] Sprite sprite_enableMusic;
    [SerializeField] Sprite sprite_disableMusic;
    [SerializeField] Sprite sprite_enableSound;
    [SerializeField] Sprite sprite_disableSound;

    string settings_music = "_Settings_Music";
    string settings_sound = "_Settings_Sound";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject); // Destroy the previous instance if it exists
            Debug.LogWarning("Multiple instances of AudioManager detected. Destroying older duplicate instance.");
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        LoadAudioSettings();
    }

    private void Start()
    {
        UpdateOptionsUI();
    }

    private void OnDestroy()
    {
        SaveAudioSettings();
    }
    

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetInt(settings_music, enableMusic ? 1 : 0);
        PlayerPrefs.SetInt(settings_sound, enableSound ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        enableMusic = (PlayerPrefs.GetInt(settings_music, 0) == 1);
        enableSound = (PlayerPrefs.GetInt(settings_sound, 0) == 1);
    }

    private void UpdateOptionsUI()
    {
        // Update the music toggle option UI based on loaded settings
        if (musicButtonImg != null)
        {
            musicButtonImg.sprite = enableMusic ? sprite_enableMusic : sprite_disableMusic;
        }
        else
        {
            Debug.LogWarning("Music Button Image is not assigned.");
        }
        // Update the sound toggle option UI based on loaded settings
        if (soundButtonImg != null)
        {
            soundButtonImg.sprite = enableSound ? sprite_enableSound : sprite_disableSound;
        }
        else
        {
            Debug.LogWarning("Sound Button Image is not assigned.");
        }
    }

    public void ToggleMusic()
    {
        enableMusic = !enableMusic;
        if (enableMusic)
        {
            musicButtonImg.sprite = sprite_enableMusic;
        }
        else
        {
            musicButtonImg.sprite = sprite_disableMusic;
        }
        AudioManager.Instance.UpdateSoundSettings();
        SaveAudioSettings();
    }

    public void ToggleSound()
    {
        enableSound = !enableSound;
        if (enableSound)
        {
            soundButtonImg.sprite = sprite_enableSound;
        }
        else
        {
            soundButtonImg.sprite = sprite_disableSound;
        }
        AudioManager.Instance.UpdateSoundSettings();
        SaveAudioSettings();
    }

}
