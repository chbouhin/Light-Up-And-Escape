using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private List<SliderManager> audioSliders;
    [HideInInspector] public ButtonInputs buttonInputs;
    private AudioManager audioManager;
    private float ratioVolume = 100f;
    public static class Volumes {
        public const int all = 0;
        public const int music = 1;
        public const int sound = 2;
    };

    private void Start()
    {
        InitVolumes();
    }

    private void Update()
    {
        if (buttonInputs != null && Input.anyKeyDown)
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKeyDown(key)) {
                    if (key == KeyCode.Mouse0)
                        StartCoroutine(WaitForMouseInput(key));
                    else
                        buttonInputs.SetInput(key);
                }
    }

    private IEnumerator WaitForMouseInput(KeyCode key)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (buttonInputs != null)
            buttonInputs.SetInput(key);
    }

    private void InitVolumes()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioSliders[Volumes.all].SetValue(audioManager.GetAllVolumes() * ratioVolume);
        audioSliders[Volumes.music].SetValue(audioManager.GetMusicVolume() * ratioVolume);
        audioSliders[Volumes.sound].SetValue(audioManager.GetSoundVolume() * ratioVolume);
    }

    public void ShowSettings(bool show)
    {
        StartCoroutine(ShowSettingsWithTransition(show));
    }

    private IEnumerator ShowSettingsWithTransition(bool show)
    {
        sceneLoader.transition.SetTrigger("HideScene");
        yield return new WaitForSecondsRealtime(sceneLoader.GetTransitionTime());
        transform.GetChild(0).gameObject.SetActive(show);
        sceneLoader.transition.SetTrigger("ShowScene");
    }

    /* ========== VOLUME ========== */

    public void SetAllVolumes()
    {
        if (!audioManager)
            return;
        float volume = audioSliders[Volumes.all].GetValue() / ratioVolume;
        audioManager.SetAllVolumes(volume);
        PlayerPrefs.SetFloat("AllVolumes", volume);
    }

    public void SetMusicVolume()
    {
        if (!audioManager)
            return;
        float volume = audioSliders[Volumes.music].GetValue() / ratioVolume;
        audioManager.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume()
    {
        if (!audioManager)
            return;
        float volume = audioSliders[Volumes.sound].GetValue() / ratioVolume;
        audioManager.SetSoundVolume(volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    /* ========== INPUT ========== */

    public void CancelInput()
    {
        buttonInputs.Cancel();
    }

    public void DeleteInput()
    {
        buttonInputs.Delete();
    }
}
