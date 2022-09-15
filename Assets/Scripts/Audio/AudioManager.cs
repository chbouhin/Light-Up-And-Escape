using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null) {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }
        sound.source.Play();
    }

    public void SetAllVolumes(float volume)
    {
        foreach (Sound sound in sounds)
            sound.source.volume = volume;
    }

    public void SetVolume(float volume, bool isMusic)
    {
        Sound[] soundsFind = Array.FindAll(sounds, s => s.isMusic == isMusic);
        foreach (Sound sound in soundsFind)
            sound.source.volume = volume;
    }
}
