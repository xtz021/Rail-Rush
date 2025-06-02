using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance {  get; private set; }

    public List<AudioSource> soundsList;
    public List<AudioSource> musicList;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void TurnSounds(bool audioState)
    {
        foreach(AudioSource source in soundsList)
        {
            if(audioState)
            {
                source.volume = 1;
            }
            else
            {
                source.volume = 0;
            }
        }
    }
    
    public void TurnMusics(bool audioState)
    {
        foreach (AudioSource source in musicList)
        {
            if (audioState)
            {
                source.volume = 1;
            }
            else
            {
                source.volume = 0;
            }
        }
    }
}
