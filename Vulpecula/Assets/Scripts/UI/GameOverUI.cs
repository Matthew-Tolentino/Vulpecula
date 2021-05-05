using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartLevel() {
        // Turn off Game Over Canvas
        transform.parent.gameObject.SetActive(false);

        // Set mouse back to game mode
        GameManager.instance.setMouseLock(true);

        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loaded Main Menu");
    }
}
