using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Popup pause;
    [SerializeField] private List<ButtonManager> buttonsInPause;
    [SerializeField] private Player square;
    [SerializeField] private Player mouseLight;
    [HideInInspector] public bool isInGame = false;
    private AudioManager audioManager;
    private bool isPause = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeMusicTheme(false);
    }

    private void Update()
    {
        if (inputManager.GetKeyDown("PauseUnpause") && isInGame != isPause)
            PauseUnpause();
    }

    public void PauseUnpause()
    {
        isPause = !isPause;
        isInGame = !isInGame;
        audioManager.Play("Pause");
        Time.timeScale = isPause ? 0f : 1f;
        if (isPause)
            foreach (ButtonManager button in buttonsInPause)
                button.Reset();
        pause.OpenClose(isPause);
    }

    public void Unpause()
    {
        isPause = true;
        PauseUnpause();
    }
}
