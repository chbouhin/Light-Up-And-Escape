using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
 
public class InputManager : MonoBehaviour
{
    private List<string> keyWords;
    private List<KeyCodes> keyCodes;
    private string filePath;

    private void Awake()
    {
        filePath = Application.dataPath + "/InputsData.json";
        CreateKeyWords();
        Load();
    }

    private void CreateKeyWords()
    {
        keyWords = new List<string>();
        keyWords.Add("SquareMoveLeft");
        keyWords.Add("SquareMoveRight");
        keyWords.Add("SquareJump");
        keyWords.Add("SquareInteract");
        keyWords.Add("MouseLightMove");
        keyWords.Add("MouseLightInteract");
        keyWords.Add("PauseUnpause");
        keyWords.Add("ChangeCamera");
    }

    private void Save()
    {
        KeysList keysList = new KeysList();
        keysList.keys = keyCodes;
        string json = JsonUtility.ToJson(keysList);        
        File.WriteAllText(filePath, json);
    }

    private void Load()
    {
        string json = File.ReadAllText(filePath);
        keyCodes = JsonUtility.FromJson<KeysList>(json).keys;
    }

    public bool GetKeyDown(string keyWord)
    {
        int index = keyWords.IndexOf(keyWord);
        return (Input.GetKeyDown(keyCodes[index].key1) || Input.GetKeyDown(keyCodes[index].key2));
    }

    public bool GetKeyUp(string keyWord)
    {
        int index = keyWords.IndexOf(keyWord);
        return (Input.GetKeyUp(keyCodes[index].key1) || Input.GetKeyUp(keyCodes[index].key2));
    }

    public bool GetKey(string keyWord)
    {
        int index = keyWords.IndexOf(keyWord);
        return (Input.GetKey(keyCodes[index].key1) || Input.GetKey(keyCodes[index].key2));
    }

    public void SetNewKey(string keyWord, bool firstKey, KeyCode keyCode)
    {
        if (firstKey)
            keyCodes[keyWords.IndexOf(keyWord)].key1 = keyCode;
        else
            keyCodes[keyWords.IndexOf(keyWord)].key2 = keyCode;
        Save();
    }

    public KeyCodes GetKeyCodes(string keyWord)
    {
        return keyCodes[keyWords.IndexOf(keyWord)];
    }
}

[System.Serializable]
public class KeysList
{
    public List<KeyCodes> keys;
}

[System.Serializable]
public class KeyCodes
{
    public KeyCode key1;
    public KeyCode key2;
}
