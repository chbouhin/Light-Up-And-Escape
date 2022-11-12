using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Popup pause;
    [SerializeField] private CoinsCount coinsCount;
    [SerializeField] private List<ButtonManager> buttonsInPause;
    [SerializeField] private Square square;
    [SerializeField] private SpriteRenderer squareSkin;
    [HideInInspector] public bool isInGame;
    private AudioManager audioManager;
    private bool isPause = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeMusicTheme(false);
        PlayerPrefs.SetInt("IsInGame", 1);
        isInGame = true;
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

    public void Victory()
    {
        isInGame = false;
        Color tempColor = squareSkin.color;
        tempColor.a = 0.5f;
        squareSkin.color = tempColor;
        audioManager.Play("Victory");
        square.StopMoving();
        string filePath = Application.dataPath + "/JsonData/LevelsData/LevelsData.json";
        string json = File.ReadAllText(filePath);
        LevelsData levelsData = JsonUtility.FromJson<LevelsData>(json);
        levelsData.coinsCount[PlayerPrefs.GetInt("LevelId", 1) - 1] = coinsCount.GetNbCoins();
        json = JsonUtility.ToJson(levelsData);
        File.WriteAllText(filePath, json);
        sceneLoader.LoadNewSceneWithDelay("LevelSelection", 2.5f);
    }

    public void Lose()
    {
        isInGame = false;
        square.StopMoving();
        sceneLoader.LoadNewSceneWithDelay("Game", 1.75f);
    }
}
