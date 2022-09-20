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
    private bool isInMenu = true;
    private float allVolumes;
    private float musicVolume;
    private float soundVolume;

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
            if (isInMenu) {
                actualMenuMusic = (actualMenuMusic + 1) % menuMusics.Length;
                music.clip = menuMusics[actualMenuMusic].clip;
            } else {
                actualGameMusic = (actualGameMusic + 1) % gameMusics.Length;
                music.clip = gameMusics[actualGameMusic].clip;
            }            
            SetMusicVolume(musicVolume);
            music.Play();
        }
    }

    private void InitAudio()
    {
        allVolumes = PlayerPrefs.GetFloat("AllVolumes", 0.5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        actualMenuMusic = UnityEngine.Random.Range(0, menuMusics.Length);
        actualGameMusic = UnityEngine.Random.Range(0, gameMusics.Length);
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

    public void ChangeMusicTheme(bool isInMenu)
    {
        if (isInMenu == this.isInMenu)
            return;
        this.isInMenu = isInMenu;
        if (isInMenu) {
            actualMenuMusic = (actualMenuMusic + 1) % menuMusics.Length;
            music.clip = menuMusics[actualMenuMusic].clip;
        } else {
            actualGameMusic = (actualGameMusic + 1) % gameMusics.Length;
            music.clip = gameMusics[actualGameMusic].clip;
        }
        SetMusicVolume(musicVolume);
        music.Play();
    }

    /* ========== AllVolumes ========== */

    public void SetAllVolumes(float volume)
    {
        allVolumes = volume;
        foreach (Sound sound in sounds)
            sound.source.volume = soundVolume * allVolumes;//si il y a 2 audiomanager, il ne detect plus l audiosource dans sounds[0].source mais detect toujours gameMusics
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
