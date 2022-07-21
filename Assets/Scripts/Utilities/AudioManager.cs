using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Array of available sounds in the scene
    public Sound[] sounds;

    void Awake()
    {
        // For each sound in the array do the following
        foreach (Sound sound in sounds)
        {
            // Attach audio source to each sound 
            sound.source = gameObject.AddComponent<AudioSource>();
            // Set audio source sound clip to sounds
            sound.source.clip = sound.clip;
            // Set audio source volume
            sound.source.volume = sound.volume;
            // Set audio source pitch
            sound.source.pitch = sound.pitch;
        }
    }

    // Play method for other scripts to call
    public void Play (string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound != null) sound.source.Play();
    }
}
