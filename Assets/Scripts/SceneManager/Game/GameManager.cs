using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Popup pause;
    [SerializeField] private Popup victory;
    [SerializeField] private CoinsCount coinsCount;
    [SerializeField] private List<ButtonManager> buttonsInPause;
    [SerializeField] private Square square;
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
        audioManager.Play("Victory");
        victory.OpenClose(true);
        square.StopMoving();
        string filePath = Application.dataPath + "/JsonData/LevelsData/LevelsData.json";
        string json = File.ReadAllText(filePath);
        LevelsData levelsData = JsonUtility.FromJson<LevelsData>(json);
        levelsData.coinsCount[PlayerPrefs.GetInt("LevelId", 1) - 1] = coinsCount.GetNbCoins();
        json = JsonUtility.ToJson(levelsData);
        File.WriteAllText(filePath, json);
    }

    public void Lose()
    {
        isInGame = false;
        square.StopMoving();
        StartCoroutine(RestartGame(1.75f));
    }

    private IEnumerator RestartGame(float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);
        sceneLoader.LoadNewScene("Game");
    }
}
