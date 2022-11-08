using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 0.5f;

    public void LoadNewScene(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        transition.SetTrigger("HideScene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public float GetTransitionTime()
    {
        return transitionTime;
    }
}
