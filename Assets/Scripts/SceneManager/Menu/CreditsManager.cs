using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Transform credits;
    [SerializeField] private GameObject defaultCanvas;
    [SerializeField] private GameObject creditsContent;
    [SerializeField] private ButtonManager speedUpBtn;
    [SerializeField] private ButtonManager creditsBtn;
    private Vector2 savePos;
    private float speed = 90f;
    private float speedMult = 4.5f;
    private bool isSpeedUp = false;
    private bool isOpen = false;

    private void Start()
    {
        savePos = credits.localPosition;
    }

    private void Update()
    {
        if (isOpen) {
            credits.position += new Vector3(0f, Time.deltaTime * speed, 0f);
            if (credits.localPosition.y >= -savePos.y) {
                ShowCredits(false);
                isOpen = false;
            }
        }
    }

    public void ShowCredits(bool show)
    {
        StartCoroutine(ShowCreditsWithTransition(show));
        if (!show) {
            creditsBtn.Reset();
            if (isSpeedUp)
                SpeedUp();
        }
    }

    public void SpeedUp()
    {
        isSpeedUp = !isSpeedUp;
        if (isSpeedUp) {
            speed *= speedMult;
        } else {
            speedUpBtn.Reset();
            speed /= speedMult;
        }
    }

    private IEnumerator ShowCreditsWithTransition(bool show)
    {
        sceneLoader.transition.SetTrigger("HideScene");
        yield return new WaitForSecondsRealtime(sceneLoader.GetTransitionTime());
        creditsContent.SetActive(show);
        defaultCanvas.SetActive(!show);
        if (!show)
            credits.localPosition = savePos;
        isOpen = show;
        sceneLoader.transition.SetTrigger("ShowScene");
    }
}
