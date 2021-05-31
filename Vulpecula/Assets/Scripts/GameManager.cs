using System.Collections;
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

    public GameObject spiritHolderUI;
    public Image mainSpiritImage;
    public Image prevSpiritImage;
    public Image nextSpiritImage;
    public Sprite floatyImg;
    public Sprite rockImg;
    public Sprite lampImg;
    public Sprite flowerPlantImg;
    public Sprite wiseImg;
    public Sprite strongImg;
    public Sprite sharkImg;
    public Sprite bushImg;
    public Sprite mushroomImg;

    public CameraSettings camSettings;

    public Vector3 playerMoveVector;

    public GameObject minimapMask;
    public GameObject minimapBorder;

    public Image fadeBackground;

    private Coroutine fadeOut;

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
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index != 0 && index != 1 && index != 3)
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
        // Play pause menu close sound
        var closeSound = pauseMenuCanvasUI.GetComponent<MenuSounds>().onCloseSound;
        if (closeSound != null) closeSound.Play();
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
        // Play pause menu open sound
        var openSound = pauseMenuCanvasUI.GetComponent<MenuSounds>().onOpenSound;
        if (openSound != null) openSound.Play();
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

    public void setSelectedSpirit(string name)
    {
        mainSpiritImage.sprite = findSpiritImage(name);
        // Make image visable
        Color temp = mainSpiritImage.color;
        if (mainSpiritImage.sprite != null)
            temp.a = 1f;
        else
            temp.a = 0f;
        mainSpiritImage.color = temp;
    }

    public void setSelectedSpiritPrev(string name)
    {
        prevSpiritImage.sprite = findSpiritImage(name);
        // Make image visable
        Color temp = prevSpiritImage.color;
        if (prevSpiritImage.sprite != null)
            temp.a = 1f;
        else
            temp.a = 0f;
        prevSpiritImage.color = temp;
    }

    public void setSelectedSpiritNext(string name)
    {
        nextSpiritImage.sprite = findSpiritImage(name);
        // Make image visable
        Color temp = nextSpiritImage.color;
        if (nextSpiritImage.sprite != null)
            temp.a = 1f;
        else
            temp.a = 0f;
        nextSpiritImage.color = temp;
    }

    public Sprite findSpiritImage(string name)
    {
        switch (name)
        {
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
            case "Plant":
                return flowerPlantImg;
            case "Strong_SpiritUI":
                return strongImg;
            case "Strong":
                return strongImg;
            case "Wise_SpiritUI":
                return wiseImg;
            case "Wise":
                return wiseImg;
            case "Shark_SpiritUI":
                return sharkImg;
            case "Sprout_SpiritUI":
                return flowerPlantImg;
            case "Bush_SpiritUI":
                return bushImg;
            case "Mushroom_SpiritUI":
                return mushroomImg;
            default:
                Debug.LogError("No sprite with name: " + name);
                return null;
        }
    }

    public void SetSpiritToolTip(string name)
    {
        TooltipTrigger tipTrigger = spiritHolderUI.GetComponent<TooltipTrigger>();
        tipTrigger.header = name;

        switch (name)
        {
            case "Rock":
                tipTrigger.content = "Will stay in one spot upon first activation and then return you upon second activation.";
                break;
            case "Bunny":
                tipTrigger.content = "Draws out the minimap to better see surroundings.";
                break;
            case "Lamp":
                tipTrigger.content = "Use on special lamps to activate a transition from solar to lunar realm.";
                break;
            case "Strong":
                tipTrigger.content = "Helps to move heavy objects.";
                break;
            case "Wise":
                tipTrigger.content = "N/A";
                break;
            case "Shark":
                tipTrigger.content = "N/A";
                break;
            case "Plant":
                tipTrigger.content = "Regenerates hp over time.";
                break;
            default:
                Debug.LogError("No spirit for trigger with name: " + name);
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

    public void GameOver()
    {
        setMouseLock(false);
        mouseState = MouseState.canvas;
        gameOverCanvasUI.SetActive(true);
    }

    public void setCameraShake(bool isShaking)
    {
        camSettings.cameraShake = isShaking;
    }

    public void setMinimapVisable(bool isOpen)
    {
        if (isOpen)
        {
            minimapMask.SetActive(true);
            minimapBorder.SetActive(true);
        }
        else
        {
            minimapMask.SetActive(false);
            minimapBorder.SetActive(false);
        }
    }

    public IEnumerator NextScene()
    {
        while (fadeBackground.color.a < 1)
        {
            Color temp = fadeBackground.color;
            temp.a += .01f;
            print("in fade in");
            yield return fadeBackground.color = temp;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        fadeOut = StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        while (fadeBackground.color.a > 0)
        {
            Color temp = fadeBackground.color;
            temp.a -= .01f;
            print("in fade out");
            yield return fadeBackground.color = temp;
        }
    }

    public void StopFadeOut() {
        StopCoroutine(fadeOut);
    }
}
