using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartLevel() {
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loaded Main Menu");
    }
}
