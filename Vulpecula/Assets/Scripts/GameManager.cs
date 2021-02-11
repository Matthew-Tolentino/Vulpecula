using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Makes GameManager into a singleton
    public static GameManager instance;

    public static bool gameIsPaused = false;

    public enum MouseState {canvas, game};
    public static MouseState mouseState = MouseState.game;

    public GameObject pauseMenuCanvasUI;
    public GameObject pauseMenuUI;

    // Make sure there is only 1 GameManager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        GameObject UI = GameObject.Find("UI");
        foreach (Transform child in UI.transform)
        {
            if (child.name == "PauseMenuCanvas") pauseMenuCanvasUI = child.gameObject;
            else if (child.name == "PausedMenuUI") pauseMenuUI = child.gameObject;
        }
    }

    //void Start()
    //{
    //    Debug.Log("Starting GameManager");
    //    GameObject UI = GameObject.Find("UI");
    //    foreach (Transform child in UI.transform)
    //    {
    //        if (child.name == "PauseMenuCanvas") pauseMenuCanvasUI = child.gameObject;
    //        else if (child.name == "PausedMenuUI") pauseMenuUI = child.gameObject;
    //    }
    //}

    void Update()
    {
        // All player key press logic goes here
        // Pause game
        if (InputManager.instance.KeyDown("Pause"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Next Dialogue
        if (InputManager.instance.KeyDown("NextDialogue"))
        {
            DialogueManager.instance.DisplayNextSentence();
        }
    }

    void Resume()
    {
        // Close pause menu, unfreeze time in game, and mark game as unpaused
        pauseMenuCanvasUI.SetActive(false);
        //Time.timeScale = 1f;
        gameIsPaused = false;

        // Lock mouse
        setMouseLock(true);

        mouseState = MouseState.game;
    }

    void Pause()
    {
        // Bring up pause menu, freeze time in game, and mark game as paused
        pauseMenuCanvasUI.SetActive(true);
        //Time.timeScale = 0f;
        gameIsPaused = true;

        // Update UI for spirits found
        Invoke(nameof(UpdateUI), 1f);

        // Unlock mouse
        setMouseLock(false);

        mouseState = MouseState.canvas;
    }

    void UpdateUI()
    {
        pauseMenuUI.GetComponent<PausedMenu>().displaySpiritsOnUI();
    }

    public void setMouseLock(bool isLocked)
    {
        if (isLocked)
        {
            // Hide mouse and lock it to center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Turn on camera control
            CameraSettings.instance.setCameraControl(true);
        }
        else
        {
            // Show mouse and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            CameraSettings.instance.setCameraControl(false);
        }
    }
}
