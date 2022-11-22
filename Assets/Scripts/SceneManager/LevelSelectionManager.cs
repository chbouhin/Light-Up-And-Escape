using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Transform levelsSelectContent;
    [SerializeField] private GameObject levelSelectButton;
    [SerializeField] private Scrollbar levelSelectScrollBar;
    [SerializeField] private CoinsAnimationManager coinsAnimationManager;

    private void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/JsonData/LevelsData/LevelsData.json");
        LevelsData levelsData = JsonUtility.FromJson<LevelsData>(json);
        int lastLevel = PlayerPrefs.GetInt("LevelId", 1);
        // Set slider pos (-34.5 is the pos to center 3 first buttons, 110 is the diff between the pos to center 1 2 3 and 4 5 6 buttons)
        levelsSelectContent.localPosition = new Vector2(0f, -34.5f + 110f * Mathf.Floor((lastLevel - 1) / 3));
        for (int count = 1; count <= levelsData.coinsCount.Count; count++) {
            GameObject newLevelSelectButton = Instantiate(levelSelectButton, levelsSelectContent);
            newLevelSelectButton.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = count.ToString();
            if (count <= PlayerPrefs.GetInt("LastLevelFinished", 0) + 1) {
                newLevelSelectButton.transform.GetChild(3).gameObject.SetActive(false);
                int tempInt = count;
                Button tempBtn = newLevelSelectButton.GetComponent<Button>();
                tempBtn.onClick.AddListener(() => LoadLevel(tempInt));
                tempBtn.interactable = true;
                newLevelSelectButton.GetComponent<Image>().color = Color.white;
                newLevelSelectButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            if (lastLevel != count || PlayerPrefs.GetInt("IsInGame", 0) == 0)
                SetCoinsColor(newLevelSelectButton, levelsData.coinsCount[count - 1]);
            else
                coinsAnimationManager.StartAnimation(newLevelSelectButton.transform.GetChild(2), levelsData.coinsCount[lastLevel - 1]);
        }
        PlayerPrefs.SetInt("IsInGame", 0);
    }

    private void SetCoinsColor(GameObject newLevelSelectButton, int nbCoins)
    {
        Transform coin = newLevelSelectButton.transform.GetChild(2);
        for (int i = 0; i < nbCoins; i++)
            coin.GetChild(i).GetComponent<Image>().color = Color.white;
    }

    public void LoadLevel(int levelId)
    {
        PlayerPrefs.SetInt("LevelId", levelId);
        PlayerPrefs.SetInt("CheckPoint", 0);
        sceneLoader.LoadNewScene("Game");
    }
}

[System.Serializable]
public class LevelsData
{
    public List<int> coinsCount;
}
