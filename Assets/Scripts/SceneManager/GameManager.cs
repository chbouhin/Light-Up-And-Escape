using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator pause;
    [SerializeField] private List<ButtonManager> buttonsInPause;
    [SerializeField] private string pauseSound;
    [SerializeField] private Player square;
    [SerializeField] private Player mouseLight;
    private AudioManager audioManager;
    private bool isPause = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeMusicTheme(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            PauseUnpause();
    }

    public void PauseUnpause()
    {
        isPause = !isPause;
        pause.SetBool("open", isPause);
        audioManager.Play(pauseSound);
        Time.timeScale = isPause ? 0f : 1f;
        if (isPause)
            foreach (ButtonManager button in buttonsInPause)
                button.Reset();
        square.SetCanMove(!isPause);
        mouseLight.SetCanMove(!isPause);
    }

    public void Unpause()
    {
        isPause = true;
        PauseUnpause();
    }
}
