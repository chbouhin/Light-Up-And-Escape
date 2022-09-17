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
    [SerializeField] private Slider sliderAllVolumes;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSound;
    [SerializeField] private TextMeshProUGUI textAllVolumes;
    [SerializeField] private TextMeshProUGUI textMusic;
    [SerializeField] private TextMeshProUGUI textSound;

    private void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        sliderAllVolumes.value = audioManager.allVolumes;
        sliderMusic.value = audioManager.musicVolume;
        sliderSound.value = audioManager.soundVolume;
        textAllVolumes.text = Mathf.FloorToInt(sliderAllVolumes.value * 100).ToString();
        textMusic.text = Mathf.FloorToInt(sliderMusic.value * 100).ToString();
        textSound.text = Mathf.FloorToInt(sliderSound.value * 100).ToString();
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
        yield return new WaitForSeconds(sceneLoader.GetTransitionTime());
        transform.GetChild(0).gameObject.SetActive(show);
        sceneLoader.transition.SetTrigger("ShowScene");
    }
}
