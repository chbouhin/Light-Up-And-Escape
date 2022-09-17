using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Music[] menuMusics;
    public Music[] gameMusics;
    private static AudioManager instance;
    private static AudioSource music;
    private int actualMenuMusic;
    private int actualGameMusic;
    private bool isInMenu = false;
    private float soundVolume = 1f;
    private float musicVolume = 1f;
    private float allVolume = 0.5f;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        InitAudio();
    }

    private void Update()
    {
        if (!music.isPlaying) {
            if (isInMenu)
                music.clip = menuMusics[actualMenuMusic].clip;
            else
                music.clip = gameMusics[actualGameMusic].clip;
            SetMusicVolume(musicVolume);
            music.Play();
        }
    }

    private void InitAudio()
    {
        actualMenuMusic = UnityEngine.Random.Range(0, menuMusics.Length);
        print(actualMenuMusic);
        print(menuMusics.Length);
        music = gameObject.AddComponent<AudioSource>();
        music.clip = menuMusics[actualMenuMusic].clip;
        music.Play();
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
        allVolume = volume;
        foreach (Sound sound in sounds)
            sound.source.volume = soundVolume * allVolume;
        music.volume = musicVolume * allVolume;
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        foreach (Sound sound in sounds)
            sound.source.volume = soundVolume * allVolume * sound.volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (isInMenu)
            music.volume = musicVolume * allVolume * menuMusics[actualMenuMusic].volume;
        else
            music.volume = musicVolume * allVolume * gameMusics[actualGameMusic].volume;
    }
}
