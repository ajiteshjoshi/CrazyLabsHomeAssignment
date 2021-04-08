using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;

    private void Start()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
        }
        PlayClip("Intro");
    }


    public void PlayClip(string name)
    {
        Sound sound = Array.Find(sounds, Sound => Sound.name == name);
        sound.audioSource.Play();                                   
    }
}
