using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 0.5f;
    private string lastScene;

    public void LoadNewScene(string sceneName)
    {
        lastScene = SceneManager.GetActiveScene().name;
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
        SceneManager.LoadScene(sceneName);
    }

    public float GetTransitionTime()
    {
        return transitionTime;
    }
}
