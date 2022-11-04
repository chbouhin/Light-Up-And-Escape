using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Transform levelsSelectContent;
    [SerializeField] private GameObject levelSelectButton;
    [SerializeField] private StarsAnimationManager starsAnimationManager;

    private void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/JsonData/LevelsData/LevelsData.json");
        LevelsData levelsData = JsonUtility.FromJson<LevelsData>(json);
        int lastLevel = PlayerPrefs.GetInt("LevelId", 1);
        for (int count = 1; count <= levelsData.starsCount.Count; count++) {
            GameObject newLevelSelectButton = Instantiate(levelSelectButton, levelsSelectContent);
            newLevelSelectButton.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = count.ToString();
            int tempInt = count;
            newLevelSelectButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(tempInt));
            if (lastLevel != count || PlayerPrefs.GetInt("IsInGame", 0) == 0)
                SetStarsColor(newLevelSelectButton, levelsData.starsCount[count - 1]);
            else
                starsAnimationManager.StartAnimation(newLevelSelectButton.transform.GetChild(2), levelsData.starsCount[PlayerPrefs.GetInt("LevelId", 1) - 1]);
        }
        PlayerPrefs.SetInt("IsInGame", 0);
    }

    private void SetStarsColor(GameObject newLevelSelectButton, int nbStars)
    {
        Transform star = newLevelSelectButton.transform.GetChild(2);
        for (int i = 0; i < nbStars; i++)
            star.GetChild(i).GetComponent<Image>().color = Color.white;
    }

    public void LoadLevel(int levelId)
    {
        PlayerPrefs.SetInt("LevelId", levelId);
        sceneLoader.LoadNewScene("Game");
    }
}

[System.Serializable]
public class LevelsData
{
    public List<int> starsCount;
}
