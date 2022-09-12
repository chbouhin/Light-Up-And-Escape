using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private float transitionTime = 1f;
    private string lastScene;

    public void LoadNewScene(string sceneName)
    {
        lastScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    public void LoadLastScene()
    {
        StartCoroutine(LoadSceneWithTransition("Menu"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneWithTransition(string sceneName)
    {
        transition.SetTrigger("HideScene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
