using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSoundsDataSO audioSoundsData;
    public List<SoundAudio> sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
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
        if (audioSoundsData == null)
        {
            Debug.LogError("AudioSoundsDataSO is not assigned in AudioManager.");
            return;
        }
        UpdateSoundSettings();
        PlayBackgroundAudio();
    }

    private void PlayBackgroundAudio()
    {
        string backgroundSoundName = null;
        if(SceneManager.GetActiveScene().buildIndex == 0) // Assuming 0 is the main menu scene
        {
            backgroundSoundName = "MenuMusic";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1) // Assuming 1 is the game scene
        {
            backgroundSoundName = "CaveMusic";
        }
        if (backgroundSoundName != null)
        {
            Play(backgroundSoundName);
        }
        else
        {
            Debug.LogWarning($"Background music name {backgroundSoundName} sound not found.");
            return;
        }
    }

    private void LoadAudioSettings()
    {
        if (audioSoundsData == null)
        {
            Debug.LogError("AudioSoundsDataSO is not assigned in AudioManager.");
            return;
        }
        sounds = audioSoundsData.GetSoundsList();
        foreach (SoundAudio s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLooping;
            s.source.mute = s.isMuted;
            s.source.playOnAwake = false; // Prevents sound from playing automatically
        }
    }

    public void Play(string name)
    {
        SoundAudio s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        SoundAudio s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Stop();
    }

    public void Mute(string name, bool mute)
    {
        SoundAudio s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.mute = mute;
        s.isMuted = mute;
    }

    public void UpdateSoundSettings()
    {
        foreach (SoundAudio s in sounds)
        {
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLooping;
            if(s.settingType == AudioSettingType.Music)
            {
                s.source.mute = !AudioSettingsHandler.Instance.enableMusic;
            }
            else if (s.settingType == AudioSettingType.Sound)
            {
                s.source.mute = !AudioSettingsHandler.Instance.enableSound;
            }
        }
    }

}
