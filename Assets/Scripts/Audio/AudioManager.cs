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
    private float allVolumes = 0.5f;
    private float musicVolume = 0.5f;
    private float soundVolume = 0.5f;

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

    /* ========== AllVolumes ========== */

    public void SetAllVolumes(float volume)
    {
        allVolumes = volume;
        foreach (Sound sound in sounds)
            sound.source.volume = soundVolume * allVolumes;
        music.volume = musicVolume * allVolumes;
    }

    public float GetAllVolumes()
    {
        return allVolumes;
    }

    /* ========== Music ========== */

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (isInMenu)
            music.volume = musicVolume * allVolumes * menuMusics[actualMenuMusic].volume;
        else
            music.volume = musicVolume * allVolumes * gameMusics[actualGameMusic].volume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    /* ========== Sound ========== */

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        foreach (Sound sound in sounds)
            sound.source.volume = soundVolume * allVolumes * sound.volume;
    }

    public float GetSoundVolume()
    {
        return soundVolume;
    }
}
