using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }
    public bool enableMusic;
    public bool enableSound;

    public List<AudioSource> soundsList;

    [SerializeField] Sprite sprite_enableMusic;
    [SerializeField] Sprite sprite_disableMusic;
    [SerializeField] Sprite sprite_enableSound;
    [SerializeField] Sprite sprite_disableSound;

    AudioSource backgroundMusic;

    string settings_music = "_Settings_Music";
    string settings_sound = "_Settings_Sound";

    private void Awake()
    {
        Instance = this;
        LoadAudioSettings();
        soundsList = new List<AudioSource>();
    }

    private void Start()
    {
        MusicCheck();
    }

    private void OnDisable()
    {
        SaveAudioSettings();
    }

    public void MusicCheck()
    {
        if (enableMusic)
        {
            backgroundMusic = GetComponent<AudioSource>();
            if (backgroundMusic == null)
            {
                Debug.LogError("Missing background muisic");
                return;
            }
            else
            {
                backgroundMusic.Play();
                backgroundMusic.mute = false;
                backgroundMusic.loop = true;
            }
        }
        else
        {
            backgroundMusic = GetComponent<AudioSource>();
            if(backgroundMusic != null)
            {
                backgroundMusic.mute = true;
            }
        }
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

}
