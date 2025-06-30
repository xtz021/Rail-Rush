using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSoundsData", menuName = "Scriptable Objects/Audio Sounds Data", order = 5)]
public class AudioSoundsDataSO : ScriptableObject
{
    public List<SoundAudio> sounds;
    // Method to get a sound by name
    public SoundAudio GetSoundByName(string name)
    {
        return sounds.Find(sound => sound.name == name);
    }

    public SoundAudio GetSoundByIndex(int index)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Index out of range: " + index);
            return null;
        }
        return sounds[index];
    }

    public List<SoundAudio> GetSoundsList()
    {
        return sounds;
    }

}