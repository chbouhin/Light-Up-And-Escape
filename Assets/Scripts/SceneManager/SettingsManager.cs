using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ButtonManager buttonToSettings;
    [SerializeField] private ButtonManager buttonToBack;
    [SerializeField] private SliderManager sliderAllVolumes;
    [SerializeField] private SliderManager sliderMusic;
    [SerializeField] private SliderManager sliderSound;
    private AudioManager audioManager;
    private float ratioVolume = 100f;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        sliderAllVolumes.SetValue(audioManager.GetAllVolumes() * ratioVolume);
        sliderMusic.SetValue(audioManager.GetMusicVolume() * ratioVolume);
        sliderSound.SetValue(audioManager.GetSoundVolume() * ratioVolume);
    }

    public void ShowSettings()
    {
        buttonToBack.Reset();
        StartCoroutine(HideShowSettingsWithTransition(true));
    }

    public void HideSettings()
    {
        buttonToSettings.Reset();
        StartCoroutine(HideShowSettingsWithTransition(false));
    }

    private IEnumerator HideShowSettingsWithTransition(bool show)
    {
        sceneLoader.transition.SetTrigger("HideScene");
        yield return new WaitForSecondsRealtime(sceneLoader.GetTransitionTime());
        transform.GetChild(0).gameObject.SetActive(show);
        sceneLoader.transition.SetTrigger("ShowScene");
    }

    /* ========== VOLUME ========== */

    public void SetAllVolumes()
    {
        if (audioManager)
            audioManager.SetAllVolumes(sliderAllVolumes.GetValue() / ratioVolume);
    }

    public void SetMusicVolume()
    {
        if (audioManager)
            audioManager.SetMusicVolume(sliderMusic.GetValue() / ratioVolume);
    }

    public void SetSoundVolume()
    {
        if (audioManager)
            audioManager.SetSoundVolume(sliderSound.GetValue() / ratioVolume);
    }
}
