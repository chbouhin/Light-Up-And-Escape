using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // Image
    [SerializeField] private Sprite spriteMouseOn;
    private Sprite spriteMouseOut;
    private Image image;

    // Button
    private RectTransform rectTransform;
    private Vector2 buttonSize;

    // Text
    [SerializeField] private bool haveText = false;
    private TextMeshProUGUI textMeshPro;
    private float textSize;

    // Animation
    private bool inAnimation = false;
    private float defaultAnimationTime = 0.15f;
    private float animationTime = 0f;

    // Sound
    [SerializeField] private string sound;
    private AudioManager audioManager;

    // Other
    private float objMinimumSize = 0.95f; // value from 0 to 1
    private bool buttonPressed = false;
    private bool buttonClicked = false;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        spriteMouseOut = image.sprite;
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        buttonSize = rectTransform.sizeDelta;
        if (haveText) {
            textMeshPro = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            textSize = textMeshPro.fontSize;
        }
    }

    private void Update()
    {
        if (!inAnimation)
            return;
        if (buttonPressed || buttonClicked) { // Start animation
            animationTime += Time.deltaTime;
            if (animationTime >= defaultAnimationTime) {
                animationTime = defaultAnimationTime;
                inAnimation = false;
            }
        } else { // End animation
            animationTime -= Time.deltaTime;
            if (animationTime <= 0) {
                animationTime = 0;
                inAnimation = false;
            }
        }
        // Set size with start or end animation
        float actualSize = Mathf.Lerp(1f, objMinimumSize, animationTime * (1 / defaultAnimationTime));
        rectTransform.sizeDelta = buttonSize * actualSize;
        if (haveText)
            textMeshPro.fontSize = textSize * actualSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = spriteMouseOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!buttonClicked)
            image.sprite = spriteMouseOut;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        inAnimation = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        inAnimation = true;
    }

    public void OnClick()
    {
        if (audioManager == null)
            audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(sound);
        buttonClicked = true;
    }
}
