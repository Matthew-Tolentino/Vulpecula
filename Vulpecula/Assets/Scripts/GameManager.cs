﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Makes GameManager into a singleton
    public static GameManager instance;

    public static bool gameIsPaused = false;

    public enum MouseState { canvas, game };
    public static MouseState mouseState = MouseState.game;

    public GameObject pauseMenuCanvasUI;
    public GameObject pauseMenuUI;
    public GameObject gameOverCanvasUI;

    public Image selectedSpiritImage;
    public Sprite floatyImg;
    public Sprite rockImg;
    public Sprite lampImg;
    public Sprite flowerPlantImg;
    public Sprite wiseImg;
    public Sprite strongImg;

    public CameraSettings camSettings;

    public Vector3 playerMoveVector;

    public GameObject minimapMask;
    public GameObject minimapBorder;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // All player key press logic goes here
        // Pause game
        if (InputManager.instance.KeyDown("Pause"))
        {
            if (gameIsPaused && SceneManager.GetActiveScene().buildIndex != 0)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Listen for keypress for next Dialogue
        if (InputManager.instance.KeyDown("NextDialogue"))
        {
            NextDialogue();
        }
    }

    void Resume()
    {
        // Close pause menu, unfreeze time in game, and mark game as unpaused
        pauseMenuCanvasUI.SetActive(false);
        //Time.timeScale = 1f;
        gameIsPaused = false;

        // Enable game control of camera (A and D)
        setMouseLock(true);

        // Set mouse state back to game
        mouseState = MouseState.game;
    }

    void Pause()
    {
        // Bring up pause menu, freeze time in game, and mark game as paused
        pauseMenuCanvasUI.SetActive(true);
        //Time.timeScale = 0f;
        gameIsPaused = true;

        // Update UI for spirits found
        if (pauseMenuUI.activeSelf)
            Invoke(nameof(UpdateUI), .25f);

        // Unlock mouse
        setMouseLock(false);

        // Set mouse state to canvas
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
            setCameraControl(true);
        }
        else
        {
            // Show mouse and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            setCameraControl(false);
        }
    }

    public void setCameraControl(bool isEnabled)
    {
        if (isEnabled)
        {
            // let mouse control camera
            camSettings.inputXAxis = "Mouse X";
            camSettings.inputYAxis = "Mouse Y";
        }
        else
        {
            camSettings.inputXAxis = "";
            camSettings.inputYAxis = "";
        }
        // else
        // {
        //     // let A and D control camera
        //     camSettings.inputXAxis = "Horizontal";
        //     camSettings.inputYAxis = "";
        // }
    }

    public void setDefaultMouseState()
    {
        mouseState = MouseState.game;
    }

    public void setSelectedSpirit(string name) {
        selectedSpiritImage.sprite = findSpiritImage(name);
        // Make image visable
        Color temp = selectedSpiritImage.color;
        if (selectedSpiritImage.sprite != null)
            temp.a = 1f;
        else
            temp.a = 0f;
        selectedSpiritImage.color = temp;
    }

    public Sprite findSpiritImage(string name) {
        switch (name) {
            case "Floaty_SpiritUI":
                return floatyImg;
            case "Floaty":
                return floatyImg;
            case "Bunny":
                return floatyImg;
            case "Rock_SpiritUI":
                return rockImg;
            case "Rock":
                return rockImg;
            case "Lamp_SpiritUI":
                return lampImg;
            case "Lamp":
                return lampImg;
            case "FlowerPlant_SpiritUI":
                return flowerPlantImg;
            case "Strong_SpiritUI":
                return strongImg;
            case "Wise_SpiritUI":
                return wiseImg;
            default:
                Debug.LogError("No sprite with name: " + name);
                return null;
        }
    }

    public void SetSpiritToolTip(string name) {
        TooltipTrigger tipTrigger = selectedSpiritImage.transform.parent.GetComponent<TooltipTrigger>();
        tipTrigger.header = name;

        switch (name) {
            case "Rock":
                tipTrigger.content = "Will stay in one spot upon first activation and then return you upon second activation.";
                break;
            case "Bunny":
                tipTrigger.content = "Draws out the minimap to better see surroundings.";
                break;
            case "Lamp":
                tipTrigger.content = "Use on special lamps to activate a transition from solar to lunar realm.";
                break;
            default:
                tipTrigger.content = "No spirit currently selected";
                break;
        }
    }

    public void NextDialogue()
    {
        if (!DialogueManager.instance.sentenceIsDone)
            DialogueManager.instance.FinishSentence();
        else
            DialogueManager.instance.DisplayNextSentence();
    }

    public void GameOver() {
        setMouseLock(false);
        mouseState = MouseState.canvas;
        gameOverCanvasUI.SetActive(true);
    }

    public void setCameraShake(bool isShaking) {
        camSettings.cameraShake = isShaking;
    }

    public void setMinimapVisable(bool isOpen) {
        if (isOpen) {
            minimapMask.SetActive(true);
            minimapBorder.SetActive(true);
        } else {
            minimapMask.SetActive(false);
            minimapBorder.SetActive(false);
        }
    }
}
