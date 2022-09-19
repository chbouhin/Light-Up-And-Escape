using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartCountdown(3));
    }

    private IEnumerator StartCountdown(int countdown)
    {
        yield return new WaitForSeconds(sceneLoader.GetTransitionTime());
        while (countdown > 0) {
            text.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        text.text = "GO";
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
