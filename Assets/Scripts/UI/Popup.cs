using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    private Transform panel;
    private Animator animator;

    private void Start()
    {
        panel = transform.GetChild(0);
        animator = panel.GetComponent<Animator>();
    }

    public void OpenClose(bool open)
    {
        if (open)
            panel.gameObject.SetActive(true);
        else
            StartCoroutine(DisablePopup());
        animator.SetBool("open", open);
        foreach (Button button in buttons)
            button.interactable = open;
    }

    private IEnumerator DisablePopup()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        if (panel.localScale.x == 0f)
            panel.gameObject.SetActive(false);
    }
}
