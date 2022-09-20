using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pause;
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
        pause.SetActive(!pause.activeSelf);
        Time.timeScale = pause.activeSelf ? 0f : 1f;
    }
}
