using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInputs : MonoBehaviour
{
    [SerializeField] private Button btnKey;
    [SerializeField] private TextMeshProUGUI textKey;
    [SerializeField] private string btnName;
    [SerializeField] private bool firstKey = true;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private GameObject PanelForInput;
    private string keyCodeSave;

    private void Start()
    {
        KeyCodes keyCodes = inputManager.GetKeyCodes(btnName);
        if (firstKey)
            SetText(keyCodes.key1);
        else
            SetText(keyCodes.key2);
    }

    private void SetText(KeyCode keyCode)
    {
        if (keyCode != KeyCode.None)
            textKey.text = keyCode.ToString();
        keyCodeSave = keyCode.ToString();
    }

    public void OnClick()
    {
        PanelForInput.SetActive(true);
        settingsManager.buttonInputs = this;
        textKey.text = "<>";
    }

    public void SetInput(KeyCode keyCode)
    {
        inputManager.SetNewKey(btnName, firstKey, keyCode);
        PanelForInput.SetActive(false);
        settingsManager.buttonInputs = null;
        SetText(keyCode);
        if (btnName == "SquareInteract" || btnName == "MouseLightInteract") {
            ShowKey[] showKeys = GameObject.FindObjectsOfType<ShowKey>();
            foreach (ShowKey showKey in showKeys)
                showKey.ChangeKey();
        }
    }

    public void Delete()
    {
        inputManager.SetNewKey(btnName, firstKey, KeyCode.None);
        textKey.text = "<Empty>";
        keyCodeSave = textKey.text;
        PanelForInput.SetActive(false);
        settingsManager.buttonInputs = null;
    }

    public void Cancel()
    {
        PanelForInput.SetActive(false);
        settingsManager.buttonInputs = null;
        textKey.text = keyCodeSave.ToString();
    }
}
