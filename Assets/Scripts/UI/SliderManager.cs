using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // Image
    [SerializeField] private Sprite spriteMouseOn;
    private Sprite spriteMouseOut;
    private Image image;

    // Other
    [SerializeField] private TextMeshProUGUI number;
    private Slider slider;
    private bool buttonPressed = false;
    private bool mouseIsOn = false;

    private void Awake()
    {
        if (!slider)
            Init();
    }

    private void Init()
    {
        slider = transform.GetComponent<Slider>();
        image = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        spriteMouseOut = image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = spriteMouseOn;
        mouseIsOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!buttonPressed)
            image.sprite = spriteMouseOut;
        mouseIsOn = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        if (!mouseIsOn)
            image.sprite = spriteMouseOut;
    }

    public void OnValueChange()
    {
        if (slider)
            number.text = slider.value.ToString();
    }

    public void SetValue(float value)
    {
        if (!slider)
            Init();
        slider.value = value;
        number.text = value.ToString();
    }

    public float GetValue()
    {
        return slider.value;
    }
}
