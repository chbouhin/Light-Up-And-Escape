using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private List<ButtonManager> buttonsInPause;
    [SerializeField] private string pauseSound;
    [SerializeField] private Square square;
    [SerializeField] private MouseLight mouseLight;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeMusicTheme(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }

    public void PauseUnpause()
    {
        audioManager.Play(pauseSound);
        pause.SetActive(!pause.activeSelf);
        Time.timeScale = pause.activeSelf ? 0f : 1f;
        if (!pause.activeSelf)
            foreach (ButtonManager button in buttonsInPause)
                button.Reset();
        square.SetCanMove(!pause.activeSelf);
        mouseLight.SetCanMove(!pause.activeSelf);
    }
}
