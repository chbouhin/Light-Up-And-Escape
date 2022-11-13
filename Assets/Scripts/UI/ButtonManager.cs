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
    [SerializeField] private RectTransform rectTransform;
    private Vector2 buttonSize;
    private bool buttonPressed = false;
    private bool buttonClicked = false;

    // Text
    [SerializeField] private bool haveText = false;
    private TextMeshProUGUI textMeshPro;
    private float textSize;

    // Animation
    [SerializeField] private bool useScale = false;
    private bool inAnimation = false;
    private float defaultAnimationTime = 0.15f;
    private float animationTime = 0f;

    // Sound
    [SerializeField] private string sound;
    private AudioManager audioManager;

    // Other
    private float objMinimumSize = 0.90f; // value from 0 to 1
    private bool mouseIsOn = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        image = transform.GetChild(0).GetComponent<Image>();
        if (spriteMouseOn)
            spriteMouseOut = image.sprite;
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
            animationTime += Time.unscaledDeltaTime;
            if (animationTime >= defaultAnimationTime) {
                animationTime = defaultAnimationTime;
                inAnimation = false;
            }
        } else { // End animation
            animationTime -= Time.unscaledDeltaTime;
            if (animationTime <= 0) {
                animationTime = 0;
                inAnimation = false;
            }
        }
        // Set size with start or end animation
        float actualSize = Mathf.Lerp(1f, objMinimumSize, animationTime * (1 / defaultAnimationTime));
        if (useScale)
            rectTransform.localScale = Vector2.one * actualSize;
        else
            rectTransform.sizeDelta = buttonSize * actualSize;
        if (haveText)
            textMeshPro.fontSize = textSize * actualSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spriteMouseOn) {
            image.sprite = spriteMouseOn;
            mouseIsOn = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (spriteMouseOn) {
            if (!buttonClicked)
                image.sprite = spriteMouseOut;
            mouseIsOn = false;
        }
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
        audioManager.Play(sound);
        buttonClicked = true;
    }

    public void Reset()
    {
        if (image) {
            buttonClicked = false;
            if (spriteMouseOn && !mouseIsOn)
                image.sprite = spriteMouseOut;
            rectTransform.sizeDelta = buttonSize;
            animationTime = 0f;
            inAnimation = false;
            if (haveText)
                textMeshPro.fontSize = textSize;
        }
    }
}
