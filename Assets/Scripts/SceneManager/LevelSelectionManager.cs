using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private Transform levelsSelectContent;
    [SerializeField] private GameObject levelSelectButton;

    private void Start()
    {
        int fileCount = System.IO.Directory.GetFiles(Application.dataPath + "/LevelsData", "Level_*.json").Length;
        for (int count = 1; count <= fileCount; count++) {
            GameObject newLevelSelectButton = Instantiate(levelSelectButton, levelsSelectContent);
            newLevelSelectButton.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = count.ToString();
        }
    }
}

