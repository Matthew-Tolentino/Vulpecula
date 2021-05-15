using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(GameManager.instance.NextScene());
    }

    private static void LoadNextScene_()
    {
        GameManager.instance.setDefaultMouseState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    // IEnumerator NextScene() {
    //     yield return StartCoroutine(GameManager.instance.FadeIn());
    //     LoadNextScene_();
    //     yield return StartCoroutine(GameManager.instance.FadeOut());
    // }
}
